#region < HEADER AREA >
// *---------------------------------------------------------------------------------------------*
//   Form ID      : WM_StockCheck
//   Form Name    : 자재 재고관리 
//   Name Space   : KFQS_Form
//   Created Date : 2020/08
//   Made By      : DSH
//   Description  : 
// *---------------------------------------------------------------------------------------------*
#endregion

#region < USING AREA >
using System;
using System.Data;
using DC_POPUP;
using DC00_assm;
using DC00_WinForm;

using Infragistics.Win.UltraWinGrid;
#endregion

namespace KFQS_Form
{
    public partial class WM_StockCheck : DC00_WinForm.BaseMDIChildForm
    {

        #region < MEMBER AREA >
        DataTable rtnDtTemp        = new DataTable(); // 
        UltraGridUtil _GridUtil    = new UltraGridUtil();  //그리드 객체 생성
        Common _Common             = new Common();
        string plantCode           = LoginInfo.PlantCode;

        #endregion


        #region < CONSTRUCTOR >
        public WM_StockCheck()
        {
            InitializeComponent();
        }
        #endregion


        #region < FORM EVENTS >
        private void WM_StockCheck_Load(object sender, EventArgs e)
        {
            #region ▶ GRID ◀
            _GridUtil.InitializeGrid(this.grid1, true, true, false, "", false);
            _GridUtil.InitColumnUltraGrid(grid1, "CHK",       "검사 선택",      true, GridColDataType_emu.CheckBox,    80, 120, Infragistics.Win.HAlign.Left,    true,  true);
            _GridUtil.InitColumnUltraGrid(grid1, "WHCODE",    "창고 코드",      true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left,    false, false);
            _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE", "공장",           true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left,    true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "LOTNO",     "LOTNO",          true, GridColDataType_emu.VarChar,    140, 120, Infragistics.Win.HAlign.Left,    true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",  "품목",           true, GridColDataType_emu.VarChar,    140, 120, Infragistics.Win.HAlign.Left,    true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME",  "품명",           true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left,    true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "STOCKQTY",  "품목 수량",      true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left,    true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "UNITCODE",  "단위",           true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left,    true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "CUSTCODE",  "거래처",         true, GridColDataType_emu.VarChar,    100, 120, Infragistics.Win.HAlign.Left,    true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "CHECKFLAG", "검사여부",       true, GridColDataType_emu.VarChar,    100, 120, Infragistics.Win.HAlign.Left,    true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "INDATE",    "입고일자",       true, GridColDataType_emu.VarChar,    100, 120, Infragistics.Win.HAlign.Left,    true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE",  "등록일시",       true, GridColDataType_emu.DateTime24, 160, 120, Infragistics.Win.HAlign.Left,    true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKER",     "등록자",         true, GridColDataType_emu.VarChar,    100, 120, Infragistics.Win.HAlign.Left,    true,  false);
            _GridUtil.SetInitUltraGridBind(grid1);

            _GridUtil.InitializeGrid(this.grid2, true, true, false, "", false);
            _GridUtil.InitColumnUltraGrid(grid2, "CHECKCODE", "검사 항목코드",     true, GridColDataType_emu.VarChar,     120,  120, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid2, "CHECKNAME", "검사 항목명",       true, GridColDataType_emu.VarChar,     140,  120, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid2, "CHECKSPEC", "검사 스펙",         true, GridColDataType_emu.VarChar,     100,  120, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid2, "EACHCHECK", "개별 항목",         true, GridColDataType_emu.VarChar,     80,   120, Infragistics.Win.HAlign.Left,  true, true);
            _GridUtil.SetInitUltraGridBind(grid2);
            #endregion

            #region ▶ COMBOBOX ◀
            rtnDtTemp = _Common.Standard_CODE("PLANTCODE");  // 사업장
            Common.FillComboboxMaster(this.cboPlantCode, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
            UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");

            rtnDtTemp = _Common.GET_ItemCodeFERT_Code("ROH");     //품목
            Common.FillComboboxMaster(this.cboItemCode, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
            UltraGridUtil.SetComboUltraGrid(this.grid1, "ITEMCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");

            rtnDtTemp = _Common.GET_TB_CUSTMATTER_CODE("");     //거래처
            Common.FillComboboxMaster(this.cboCustCode, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
            UltraGridUtil.SetComboUltraGrid(this.grid1, "CUSTCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");

            rtnDtTemp = _Common.Standard_CODE("MEASURETOTJUD");  // 개별항목
            UltraGridUtil.SetComboUltraGrid(this.grid2, "EACHCHECK", rtnDtTemp, "CODE_ID", "CODE_NAME");


            #endregion

            #region ▶ POP-UP ◀
            BizTextBoxManager btbManager = new BizTextBoxManager();
            btbManager.PopUpAdd(txtWorkerID, txtWorkerName, "WORKER_MASTER", new object[] { "", "", "", "", "" });
            #endregion

            #region ▶ ENTER-MOVE ◀
            cboPlantCode.Value = plantCode;
            #endregion
        }
        #endregion


        #region < TOOL BAR AREA >
        public override void DoInquire()
        {
            DoFind();
        }
        private void DoFind()
        {
            DBHelper helper = new DBHelper(false);
            try
            {
                _GridUtil.Grid_Clear(grid1);
                _GridUtil.Grid_Clear(grid2);

                string sPlantCode  = Convert.ToString(cboPlantCode.Value);
                string sItemCode   = Convert.ToString(cboItemCode.Value);
                string sLotNo      = Convert.ToString(txtLotNo.Text);
                string sCustCode   = Convert.ToString(cboCustCode.Value);
                string sStartDate  = string.Format("{0:yyyy-MM-dd}", dtStartDate.Value);
                string sEndDate    = string.Format("{0:yyyy-MM-dd}", dtEnddate.Value);

                rtnDtTemp = helper.FillTable("1JO_StockCheck_S1", CommandType.StoredProcedure
                                    , helper.CreateParameter("PLANTCODE",  sPlantCode,  DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("ITEMCODE",   sItemCode,   DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("LOTNO",      sLotNo,      DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("CUSTCODE",   sCustCode,   DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("DATESTART",  sStartDate,  DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("DATEEND",    sEndDate,    DbType.String, ParameterDirection.Input)
                                    );

               this.ClosePrgForm();
               this.grid1.DataSource = rtnDtTemp;
            }
            catch (Exception ex)
            {
                ShowDialog(ex.ToString(),DialogForm.DialogType.OK);    
            }
            finally
            {
                helper.Close();
            }
        }
        /// <summary>
        /// ToolBar의 신규 버튼 클릭
        /// </summary>
        public override void DoNew()
        {
            
        }
        /// <summary>
        /// ToolBar의 삭제 버튼 Click
        /// </summary>
        public override void DoDelete()
        {   
           
        }
        /// <summary>
        /// ToolBar의 저장 버튼 Click
        /// </summary>
        public override void DoSave()
        {
            this.grid1.UpdateData();
            DataTable dt = grid1.chkChange();
            DataTable dt2 = (DataTable)grid2.DataSource;

            string sWorkerId = txtWorkerID.Text.ToString();
            if (sWorkerId == "")
            {
                ShowDialog("작업자를 선택 후 진행하세요.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }

            if (dt == null)
            {
                ShowDialog("선택된 출고 내역이 없습니다.", DialogForm.DialogType.OK);
                return;
            }

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                if (Convert.ToString(dt2.Rows[i]["EACHCHECK"]) == "")
                {
                    ShowDialog("개별 검사 항목을 선택하지 않은 행이 있습니다.", DialogForm.DialogType.OK);
                    return;
                }
            }


            DBHelper helper = new DBHelper("", true);
            try
            {
                if (this.ShowDialog("선택하신 내역을 출고 등록 하시겠습니까 ?") == System.Windows.Forms.DialogResult.Cancel) return;

                string sCheckSEQ = string.Empty;
                string sTester = Convert.ToString(txtWorkerID.Text);
                foreach (DataRow drRow in dt2.Rows)
                {
                    helper.ExecuteNoneQuery("1JO_StockCheck_U1", CommandType.StoredProcedure
                                                  , helper.CreateParameter("PLANTCODE", Convert.ToString(dt.Rows[0]["PLANTCODE"]), DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("LOTNO",     Convert.ToString(dt.Rows[0]["LOTNO"]), DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("ITEMCODE",  Convert.ToString(dt.Rows[0]["ITEMCODE"]), DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("CHECKCODE", Convert.ToString(drRow["CHECKCODE"]), DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("EACHCHECK", Convert.ToString(drRow["EACHCHECK"]), DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("TESTER",    sTester, DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("CHECKSEQ",  sCheckSEQ,  DbType.String, ParameterDirection.Input)
                                                  );

                    if (helper.RSCODE == "S")
                    {
                        sCheckSEQ = helper.RSMSG;
                    }
                    if (helper.RSCODE != "S") break;

                }

                helper.ExecuteNoneQuery("1JO_StockCheck_U2", CommandType.StoredProcedure
                                                  , helper.CreateParameter("PLANTCODE", Convert.ToString(dt.Rows[0]["PLANTCODE"]), DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("LOTNO",     Convert.ToString(dt.Rows[0]["LOTNO"]),     DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("WHCODE",    Convert.ToString(dt.Rows[0]["WHCODE"]),    DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("ITEMCODE",  Convert.ToString(dt.Rows[0]["ITEMCODE"]),  DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("TESTER",    sTester,                                   DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("CHECKSEQ",  sCheckSEQ,                                 DbType.String, ParameterDirection.Input)
                                                  );

                if (helper.RSCODE != "S")
                {
                    this.ClosePrgForm();
                    helper.Rollback();
                    this.ShowDialog(helper.RSMSG, DialogForm.DialogType.OK);
                    return;
                }
                helper.Commit();
                this.ClosePrgForm();
                this.ShowDialog("데이터가 저장 되었습니다.", DialogForm.DialogType.OK);
                DoInquire();
            }
            catch (Exception ex)
            {
                CancelProcess = true;
                helper.Rollback();
                ShowDialog(ex.ToString());
            }
            finally
            {
                helper.Close();
            }
        }


        #endregion

        private void grid1_AfterRowActivate(object sender, EventArgs e)
        {
            DBHelper helper = new DBHelper(false);
            try
            {
                rtnDtTemp = helper.FillTable("1JO_StockCheck_S2", CommandType.StoredProcedure
                                    );

                this.grid2.DataSource = rtnDtTemp;
            }
            catch (Exception ex)
            {
                ShowDialog(ex.ToString(), DialogForm.DialogType.OK);
            }
            finally
            {
                helper.Close();
            }
        }
    }
}




