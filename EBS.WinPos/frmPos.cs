﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EBS.WinPos.Domain.Entity;
using EBS.WinPos.Domain;
using EBS.WinPos.Service;
using EBS.WinPos.Service.Dto;
using EBS.Infrastructure;
namespace EBS.WinPos
{
    public partial class frmPos : Form
    {


        SaleOrderService _saleOrderService;
        ProductService _productService;
        VipCardService _vipService;
        VipProductService _vipProductService;
        ShopCart _currentShopCat;

        public int OrderId { get; set; }

        public VipCard VipCustomer { get; set; }

        public frmPos()
        {
            InitializeComponent();

            _saleOrderService = new SaleOrderService();
            _productService = new ProductService();
            _vipService = new VipCardService();
            _vipProductService = new VipProductService();
          
        }

        frmPay _payForm;
        public void inputEnter()
        {
            var input = this.txtBarCode.Text;
            //商品编码固定8位，少于8位，即认为是输入的金额
            if (input.Length <= 7)
            {
                if (this._currentShopCat.OrderAmount > 0) {
                    CreateSaleOrder(input);
                }
                else if (this._currentShopCat.OrderAmount < 0){
                    CreateBackOrder();
                }
                else {
                    MessageBox.Show("订单金额不能为 0！", "系统消息", MessageBoxButtons.OK, MessageBoxIcon.Information); return;
                }               
            }
            else
            {
                InputBarCode(input);
            }
        }

        public void InputBarCode(string productCodeOrBarCode)
        {          
            Product model = _productService.GetProduct(productCodeOrBarCode);
            if (model == null) { 
                MessageBox.Show("商品不存在"); 
                this.txtBarCode.Text = ""; 
                return;
            }
            this._currentShopCat = this._currentShopCat ?? new ShopCart(ContextService.CurrentAccount.StoreId, ContextService.CurrentAccount.Id);
            //查询会员折扣
            var discount = this.VipCustomer == null ? 1 : this.VipCustomer.Discount;
            var vipProduct=_vipProductService.GetByProductId(model.Id);
            //真正的销售价
            var realPrice = model.SalePrice;
            if(this.VipCustomer!=null)
            {
                realPrice = vipProduct == null ? model.SalePrice * discount : vipProduct.SalePrice;
            }          
            //加入购物车
            this._currentShopCat.AddShopCart(new ShopCartItem(model, 1, realPrice));          

            ShowOrderInfo();
            this.txtBarCode.Text = "";
            var lastIndex = this.dgvData.Rows.Count - 1;
            this.dgvData.Rows[lastIndex].Selected = true;

        }

        public void ShowOrderInfo()
        {
            this.lblOrderTotal.Text = "总金额：" + _currentShopCat.OrderAmount.ToString("C");
            this.lblQuantityTotal.Text = "总件数：" + _currentShopCat.TotalQuantity.ToString();
            this.lblDiscount.Text = "总优惠：" + _currentShopCat.TotalDiscountAmount.ToString("C");
            //刷新gridview
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.DataSource =new List<ShopCartItem>();
            this.dgvData.DataSource = this._currentShopCat.Items;
            this.dgvData.ClearSelection();
            this.dgvData.FirstDisplayedScrollingRowIndex = this.dgvData.RowCount - 1;

        }

        public void ShowPreOrderInfo()
        {
            var model = this._currentShopCat;
            this.lblPreOrderCode.Text ="上一单：" + model.OrderCode;
            this.lblPreOrderAmount.Text = "金额：" + model.OrderAmount.ToString("C");
            this.lblPreChargeAmount.Text = "找零:" + model.ChargeAmount.ToString("C");
        }

        public void CreateSaleOrder(string money)
        {
            try
            {
                var inputAmount = 0m;
                decimal.TryParse(money, out inputAmount);
                _currentShopCat.PayAmount = inputAmount;
                _saleOrderService.CreateOrder(_currentShopCat);            
                this.lblOrderCode.Text ="订单号："+ _currentShopCat.OrderCode;
                this.txtBarCode.Text = "";

                // 显示支付窗体  
                if (_currentShopCat.CheckCanPay())
                {
                    _payForm = new frmPay();
                    _payForm.CurrentOrder = _currentShopCat;
                    _payForm.PosForm = this;
                    _payForm.ShowDialog(this);
                }
                else {
                    MessageBox.Show("有商品项数量为0,不能支付！请调整数量或ESC作废订单。");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CreateBackOrder()
        {           
            // 退单
            DialogResult result = MessageBox.Show("你确定保存销售退单？", "系统信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return;
            }
            _currentShopCat.OrderType = 2; // 销售退单
            _saleOrderService.CreateOrder(_currentShopCat);
            ShowPreOrderInfo();
            ClearAll();
            //打印小票
            _saleOrderService.PrintTicket(_currentShopCat.OrderId);
        }
        /// <summary>
        /// 按ESC 取消订单
        /// </summary>
        public void CreateCancelOrder()
        {
            try
            {
                DialogResult result = MessageBox.Show("你确定作废订单？", "系统信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    return;
                }
                if (this._currentShopCat.OrderId == 0)
                {
                    _saleOrderService.CreateOrder(_currentShopCat);
                }
                _saleOrderService.CancelOrder(this.OrderId, ContextService.CurrentAccount.Id);
                MessageBox.Show("订单作废成功", "系统消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //打印小票
                _saleOrderService.PrintTicket(_currentShopCat.OrderId);
                ShowPreOrderInfo();
                this.ClearAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // 打印作废小票
        }



        public void ClearAll()
        {
            this.dgvData.DataSource = new List<ShopCartItem>();
            this.lblOrderTotal.Text = "总金额:￥0.00";
            this.lblQuantityTotal.Text = "总件数：0";
            this.lblOrderCode.Text = "订单号：";
            this.lblDiscount.Text = "总优惠:￥0.00";
            this.VipCustomer = null;
            this._currentShopCat = null;
        }

        private void txtBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    inputEnter();
                    break;
                case Keys.Escape:
                    CreateCancelOrder();
                    break;
                case Keys.F1:  // +
                    ModifyQuantity();
                    break;
                case Keys.F2:  // - 
                   // MinusQuantity();
                    inputCustomer();
                    break;
                case Keys.Q:
                    Quit();
                    break;               
                default:
                    break;
            }
        }

        public void Quit()
        { 
             this.Close();
             var mainForm = ContextService.ParentForm;
             if (mainForm == null)
             {
                 mainForm = new frmMain();
                 ContextService.AddFrom(mainForm);
                 mainForm.Show();
             }
             else
             {
                 mainForm.Show();
             }
        }

       

        public void ModifyQuantity()
        {
            int lastIndex = this.dgvData.Rows.GetLastRow(DataGridViewElementStates.Selected);
            if (lastIndex < 0) {
                throw new AppException("请选择要修改数量的商品");
            }
            //设置焦点
            this.dgvData.Focus();
            this.dgvData.CurrentCell = this.dgvData.Rows[lastIndex].Cells["Quantity"];
            this.dgvData.BeginEdit(true);

        }

       

        public void inputCustomer()
        {
            frmVipCard vipForm = new frmVipCard();
            vipForm.PosFrom = this;
            vipForm.ShowDialog(this);
        }

        public void SetVipCard(string code)
        {
            this.VipCustomer = _vipService.GetByCode(code);
            //刷新当前购物车折扣  
            var discount = this.VipCustomer == null ? 1 : this.VipCustomer.Discount;
            this._currentShopCat = this._currentShopCat ?? new ShopCart(ContextService.CurrentAccount.StoreId,ContextService.CurrentAccount.Id);
            foreach (var item in this._currentShopCat.Items)
            {
                var vipProduct = _vipProductService.GetByProductId(item.ProductId);
                //真正的销售价
                var realPrice = item.SalePrice;
                if (this.VipCustomer != null)
                {
                    realPrice = vipProduct == null ? item.SalePrice * discount : vipProduct.SalePrice;
                }
                item.ChangeRealPrice(realPrice);
            }
            // 重新绑定
            this.ShowOrderInfo();
            if (this.dgvData.CurrentCell != null)
            {
                this.dgvData.CurrentCell.Selected = true;
            }
            this.txtBarCode.Focus();
        }

        private void frmPos_Load(object sender, EventArgs e)
        {
            // 如果没有点上班，不显示该界面
            this.lblAccountId.Text = "工号：" + ContextService.CurrentAccount.Id;
            this.lblStoreId.Text = "门店：" + ContextService.CurrentAccount.StoreId;
            // this.lblStoreId.Text = "门店："
            lblKeys.Text = "快捷键：F1 改数量,F2 会员,ESC 作废订单 ";
            this.txtBarCode.Focus();
            this.dgvData.ClearSelection();


        }

        private void dgvData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X,
           e.RowBounds.Location.Y,
           dgvData.RowHeadersWidth - 4,
           e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dgvData.RowHeadersDefaultCellStyle.Font,
                rectangle,
                Color.Black,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void frmPos_Resize(object sender, EventArgs e)
        {
           
        }

        //dgvData.RowHeadersDefaultCellStyle.ForeColor

        private void TextBoxDec_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 45  - 号
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 45) //&& e.KeyChar != '.' e.KeyChar != 8 &&
            {
                e.Handled = true;
            }
        }

        private void dgvData_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (this.dgvData.CurrentCell.ColumnIndex == 8)  //数量列
            {
                e.Control.KeyPress += new KeyPressEventHandler(TextBoxDec_KeyPress);

                //DataGridViewTextBoxColumn
                //if (e.Control.GetType().Name == "DataGridViewTextBoxEditingControl")
                //{
                //    var qtyBox = e.Control as DataGridViewTextBoxEditingControl;
                //    //添加事件
                //    qtyBox.TextChanged += qtyBox_TextChanged;
                //}
            }
        }

        void qtyBox_TextChanged(object sender, EventArgs e)
        {           
            //int column = dgvData.CurrentCellAddress.X;
            //int row = dgvData.CurrentCellAddress.Y;
            //var qtyBox = (DataGridViewTextBoxEditingControl)sender;
            //if (string.IsNullOrEmpty(qtyBox.Text))
            //{
            //    return;
            //}
            //var quantity = Convert.ToInt32(qtyBox.Text);
            //var pid = this.dgvData.Rows[row].Cells["ProductId"].Value.ToString();
            //var productId = Convert.ToInt32(pid);
            //this._currentShopCat.ChangeQuantity(productId,quantity);

            //this.dgvData[column, row].Value = qtyBox.Text;
            //this.lblOrderTotal.Text = "总金额：" + _currentShopCat.OrderAmount.ToString("C");
            //this.lblQuantityTotal.Text = "总件数：" + _currentShopCat.TotalQuantity.ToString();
            //this.lblDiscount.Text = "总优惠：" + _currentShopCat.TotalDiscountAmount.ToString("C");
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.dgvData.IsCurrentCellInEditMode)   //如果当前单元格处于编辑模式   
                {
                    var index = this.dgvData.CurrentCell.RowIndex;
                    var quantity = Convert.ToInt32(this.dgvData.Rows[index].Cells["Quantity"].EditedFormattedValue.ToString());
                    var pid = this.dgvData.Rows[index].Cells["ProductId"].Value.ToString();
                    var productId = Convert.ToInt32(pid);
                    this._currentShopCat.ChangeQuantity(productId, quantity);
                    this.ShowOrderInfo(); //重新刷新                  
                    this.dgvData.Rows[index].Selected = true;
                    txtBarCode.Focus();
                }               
            }           
             return base.ProcessCmdKey(ref msg, keyData);
        }

        // this.panel1.Height = 36;
        // this.panel3.Height = 180;
        // this.panel2.Height = this.Height - this.panel1.Height - this.panel3.Height;
    }
}