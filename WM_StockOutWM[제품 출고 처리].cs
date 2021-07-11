#region < HEADER AREA >
// *---------------------------------------------------------------------------------------------*
//   Form ID      : WM_StockOutWM
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
    public partial class WM_StockOutWM : DC00_WinForm.BaseMDIChildForm
    {

        #region < MEMBER AREA >
        DataTable rtnDtTemp        = new DataTable(); // 
        UltraGridUtil _GridUtil    = new UltraGridUtil();  //그리드 객체 생성
        Common _Common             = new Common();
        string plantCode           = LoginInfo.PlantCode;

        #endregion


        #region < CONSTRUCTOR >
        public WM_StockOutWM()
        {
            InitializeComponent();
        }
        #endregion


        #region < FORM EVENTS >
        private void WM_StockOutWM_Load(object sender, EventArgs e)
        {
            #region ▶ GRID1 ◀
            _GridUtil.InitializeGrid(this.grid1, true, true, false, "", false);
            _GridUtil.InitColumnUltraGrid(grid1, "CHK",            "출고등록",       true, GridColDataType_emu.CheckBox,    80, 120, Infragistics.Win.HAlign.Left, true, true);
            _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",      "공장",           true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "SHIPNO",         "상차번호",       true, GridColDataType_emu.VarChar,    140, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "SHIPDATE",       "상차일자",       true, GridColDataType_emu.VarChar,    140, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "CARNO",          "차량번호",       true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "CUSTCODE",       "거래처코드",     true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "CUSTNAME",       "거래처명",       true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKER",         "상차자",         true, GridColDataType_emu.VarChar,    100, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "TRADINGNO",      "명세서",         true, GridColDataType_emu.VarChar,    100, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "TRADINGDATE",    "출고일자",       true, GridColDataType_emu.VarChar,    100, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE",       "등록일시",       true, GridColDataType_emu.DateTime24, 160, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKER",          "등록자",         true, GridColDataType_emu.VarChar,    100, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "EDITDATE",       "수정일시",       true, GridColDataType_emu.VarChar,    160, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "EDITOR",         "수정자",         true, GridColDataType_emu.VarChar,    100, 120, Infragistics.Win.HAlign.Left, true, false);


            _GridUtil.SetInitUltraGridBind(grid1);
            #endregion

            #region ▶ GRID2 ◀
            _GridUtil.InitializeGrid(this.grid2, true, true, false, "", false);
            _GridUtil.InitColumnUltraGrid(grid2, "PLANTCODE",      "공장",           true, GridColDataType_emu.VarChar, 120, 120, Infragistics.Win.HAlign.Left,   false, false);
            _GridUtil.InitColumnUltraGrid(grid2, "SHIPNO",         "상차번호",       true, GridColDataType_emu.VarChar, 140, 120, Infragistics.Win.HAlign.Left,   false, false); //뒤에서 두번째가 비지블
            _GridUtil.InitColumnUltraGrid(grid2, "SHIPSEQ",        "품목명",         true, GridColDataType_emu.VarChar, 80,  120, Infragistics.Win.HAlign.Center, true, false);
            _GridUtil.InitColumnUltraGrid(grid2, "LOTNO",          "LOTNO",          true, GridColDataType_emu.VarChar, 120, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid2, "ITEMCODE",       "품목",           true, GridColDataType_emu.VarChar, 120, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid2, "ITEMNAME",       "품명",           true, GridColDataType_emu.VarChar, 150, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid2, "SHIPQTY",        "상차수량",       true, GridColDataType_emu.Double,  100, 120, Infragistics.Win.HAlign.Right,  true, false);
            _GridUtil.InitColumnUltraGrid(grid2, "UNITCODE",       "단위",           true, GridColDataType_emu.VarChar, 100, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.SetInitUltraGridBind(grid2);
            #endregion

            #region ▶ COMBOBOX ◀
            rtnDtTemp = _Common.Standard_CODE("PLANTCODE");  // 사업장
            Common.FillComboboxMaster(this.cboPlantCode, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", ""); //이 구문은 왜 넣?
            UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");
 
            rtnDtTemp = _Common.Standard_CODE("UNITCODE");  // 단위
            UltraGridUtil.SetComboUltraGrid(this.grid2, "UNITCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");
            #endregion

            #region ▶ POP-UP ◀
            BizTextBoxManager btbManager = new BizTextBoxManager();
            btbManager.PopUpAdd(txtCustCode_H, txtCustName_H, "CUST_MASTER", new object[] { cboPlantCode, "", "", "" });
            #endregion

            #region ▶ ENTER-MOVE ◀
            cboPlantCode.Value = plantCode;
            #endregion
        }
        #endregion


        #region ▶ TOOL BAR AREA ◀
        public override void DoInquire()
        {
            DoFind();
        }
        private void DoFind()
        {
            DBHelper helper = new DBHelper(false);
            try
            {
                base.DoInquire();
                _GridUtil.Grid_Clear(grid1); //초기화
                _GridUtil.Grid_Clear(grid2); //초기화
                string sPlantCode  = Convert.ToString(cboPlantCode.Value);
                string sCustCode   = Convert.ToString(txtCustCode_H.Value);
                string sCarNo      = Convert.ToString(txtCustCode_H.Value);
                string sShipNo     = Convert.ToString(txtShipNo.Text);
                string sStartDate  = string.Format("{0:yyyy-MM-dd}", dtStartDate.Value);
                string sEndDate    = string.Format("{0:yyyy-MM-dd}", dtEnddate.Value);

                rtnDtTemp = helper.FillTable("14WM_StockOutWM_S1", CommandType.StoredProcedure
                                    , helper.CreateParameter("PLANTCODE", sPlantCode, DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("CUSTCODE",  sCustCode,  DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("STARTDATE", sStartDate, DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("ENDDATE",   sEndDate,   DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("CARNO",     sCarNo,     DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("SHIPNO",    sShipNo,    DbType.String, ParameterDirection.Input)
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
            if (dt == null)
                return;
            string sCarNo = Convert.ToString(dt.Rows[0]["CARNO"]);
            int ChkCount = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToString(dt.Rows[i]["CHK"]) != "1") continue;
                if (sCarNo != Convert.ToString(dt.Rows[i]["CARNO"]))
                {
                    ShowDialog("차량 번호가 동일하지 않은 출고 등록 및 거래명세서는 발행 할 수 없습니다.", DialogForm.DialogType.OK);
                    return;
                }
                ChkCount += 1;

                if (ChkCount ==0)
                {
                    ShowDialog("선택된 출고 내역이 없습니다.", DialogForm.DialogType.OK);
                }
            }
            DBHelper helper = new DBHelper("", true);
            try
            {
                // 동일한 차량 번호만 선택 하였는지 확인!

                if (this.ShowDialog("선택하신 내역을 출고 등록 하시겠습니까 ?") == System.Windows.Forms.DialogResult.Cancel) return;

                string sTradingNo = string.Empty;
                foreach (DataRow drRow in dt.Rows)
                {
                    switch (drRow.RowState)
                    {
                        case DataRowState.Deleted:
                            #region 삭제 
                            #endregion
                            break;
                        case DataRowState.Added:
                            #region 추가

                            #endregion
                            break;
                        case DataRowState.Modified:
                            #region 수정
                            if (Convert.ToString(drRow["CHK"]) != "1") continue;
                            helper.ExecuteNoneQuery("14WM_StockOutWM_U1", CommandType.StoredProcedure
                                                  , helper.CreateParameter("PLANTCODE", Convert.ToString(drRow["PLANTCODE"]), DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("SHIPNO", Convert.ToString(drRow["SHIPNO"]), DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("TRADINGNO", sTradingNo, DbType.String, ParameterDirection.Input)
                                                  //공통상태를 만들어주기 위해서 TRADINNO를 던진겁니다.
                                                  , helper.CreateParameter("MAKER", LoginInfo.UserID, DbType.String, ParameterDirection.Input)
                                                  );

                            if (helper.RSCODE == "S")
                            {
                                sTradingNo = helper.RSMSG;
                            }
                            else break;
                            #endregion
                            break;
                    }
                    if (helper.RSCODE != "S") break;
                }
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

        #region ▶ GRID2 DATA가져오기 ◀
        private void grid1_AfterRowActivate(object sender, EventArgs e)
        {
            DBHelper helper = new DBHelper(false);
            try
            {
                string sPlantCode = Convert.ToString(grid1.ActiveRow.Cells["PLANTCODE"].Value);
                string sShipNo    = Convert.ToString(grid1.ActiveRow.Cells["SHIPNO"].Value);
                //상차 상세내역의 데이터를 가져올 것입니다.
                //GRID1에서 위 2개만 가져올 것입니다.

                rtnDtTemp = helper.FillTable("14WM_StockOutWM_S2", CommandType.StoredProcedure
                                    , helper.CreateParameter("PLANTCODE", sPlantCode, DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("SHIPNO",    sShipNo,    DbType.String, ParameterDirection.Input)
                                    );


                this.ClosePrgForm(); //안넣어주면 조회구문이 계속돌아가기 때문에 넣어줘야합니다.
                this.grid2.DataSource = rtnDtTemp;
                //가져온 DATA를 GRID2에 가져와서 바인딩해줍니다.
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
        #endregion
    }
}




