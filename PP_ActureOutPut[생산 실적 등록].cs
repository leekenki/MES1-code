using DC00_assm;
using DC_POPUP;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KFQS_Form
{
    public partial class PP_ActureOutPut : DC00_WinForm.BaseMDIChildForm
    {
        // 그리드를 셋팅 할 수 있도록 도와주는 함수 클래스
        UltraGridUtil _GridUtil = new UltraGridUtil();
        //공장 변수 입력
        //private sPlantCode = LoginInfo.
        public PP_ActureOutPut()
        {
            InitializeComponent();
        }

        private void PP_ActureOutPut_Load(object sender, EventArgs e)
        {
            // 그리드를 셋팅한다.
            try
            {
                _GridUtil.InitializeGrid(this.grid1, false, true, false, "", false);
                _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",      "공장",                true, GridColDataType_emu.VarChar,    130, 130, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ORDERNO",        "작업지시 번호",       true, GridColDataType_emu.VarChar,    130, 130, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",       "품목 코드",           true, GridColDataType_emu.VarChar,    130, 130, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "PLANQTY",        "계획수량",            true, GridColDataType_emu.Double,     130, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "PRODQTY",        "양품수량",            true, GridColDataType_emu.Double,     130, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "BADQTY",         "불량수량",            true, GridColDataType_emu.Double,     130, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "UNITCODE",       "단위",                true, GridColDataType_emu.VarChar,    130, 130, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "MATLOTNO",       "투입로트",            true, GridColDataType_emu.VarChar,    130, 130, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "COMPONENT",      "투입품목",            true, GridColDataType_emu.VarChar,    130, 130, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "COMPONENTQTY",   "투입 수량",           true, GridColDataType_emu.Double,     130, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "CUNITCODE",      "투입단위",            true, GridColDataType_emu.VarChar,    130, 130, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERCODE", "작업장",              true, GridColDataType_emu.VarChar,    130, 130, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "WORKSTATUSCODE", "상태코드",            true, GridColDataType_emu.VarChar,    130, 130, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "WORKSTATUS",     "상태",                true, GridColDataType_emu.VarChar,    130, 130, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "WORKER",         "작업자",              true, GridColDataType_emu.VarChar,    130, 130, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "WORKERNAME",     "작업자명",            true, GridColDataType_emu.VarChar,    130, 130, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "STARTDATE",      "최초 가동 시작 시간", true, GridColDataType_emu.DateTime24, 160, 130, Infragistics.Win.HAlign.Left,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ENDDATE",        "작업지시  종료 시간", true, GridColDataType_emu.DateTime24, 160, 130, Infragistics.Win.HAlign.Left,  true, false);
                //셋팅 내역 그리드와 바인딩
                _GridUtil.SetInitUltraGridBind(grid1); //셋팅 내역 그리드와 바인딩

                Common _Common = new Common();
                DataTable dtTemp = new DataTable();
                // PLANTCODE 기준정보 가져와서 데이터 테이블에 추가.
                dtTemp = _Common.Standard_CODE("PLANTCODE");
                // 데이터 테이블에 있는 데이터를 해당 콤보박스에 추가.
                Common.FillComboboxMaster(this.cboPlantCode, dtTemp,
                                          dtTemp.Columns["CODE_ID"].ColumnName,
                                          dtTemp.Columns["CODE_NAME"].ColumnName,
                                          "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", dtTemp, "CODE_ID", "CODE_NAME");

                // 작업장 마스터 데이터 가져와서 임시 테이블에 등록
                dtTemp = _Common.GET_Workcenter_Code();  // 작업장

                // 콤보박스 컨트롤에 가져온 데이터 등록
                Common.FillComboboxMaster(this.cboWorkCenterCode,
                                          dtTemp, dtTemp.Columns["CODE_ID"].ColumnName,
                                          dtTemp.Columns["CODE_NAME"].ColumnName,
                                          "ALL", "");

                // 그리드 해당 컬럼에 콤보박스 유형으로 셋팅
                UltraGridUtil.SetComboUltraGrid(this.grid1, "WORKCENTERCODE", dtTemp, "CODE_ID", "CODE_NAME");

                #region ▶ POP-UP ◀
                BizTextBoxManager btbManager = new BizTextBoxManager();
                btbManager.PopUpAdd(txtWorkerId,
                                    txtWorkerName,
                                    "WORKER_MASTER"
                                    , new object[] { "", "", "", "", "" });
                #endregion

            }
            catch (Exception ex)
            {
                ShowDialog(ex.Message, DC00_WinForm.DialogForm.DialogType.OK);
            }
        }

        public override void DoInquire()
        {
            base.DoInquire();
            DBHelper helper = new DBHelper(false);
            try
            {
                string sPlantCode      = Convert.ToString(cboPlantCode.Value);
                string sWorkcentercode = Convert.ToString(cboWorkCenterCode.Value);
                string sStartDate      = string.Format("{0:yyyy-MM-dd}", dtpStart.Value);
                string sEndDate        = string.Format("{0:yyyy-MM-dd}", dtpEnd.Value);
                string sOrderNo        = Convert.ToString(txtOrderNo.Text);


                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("14PP_ActureOutPut_S1", CommandType.StoredProcedure
                                          , helper.CreateParameter("PLANTCODE",      sPlantCode,      DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("WORKCENTERCODE", sWorkcentercode, DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("STARTDATE",      sStartDate,      DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("ENDDATE",        sEndDate,        DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("ORDERNO",        sOrderNo,        DbType.String, ParameterDirection.Input)
                                          );
                this.ClosePrgForm();
                if (dtTemp.Rows.Count > 0)
                {
                    grid1.DataSource = dtTemp;
                    grid1.DataBinds(dtTemp);
                }
                else
                {
                    _GridUtil.Grid_Clear(grid1);
                    ShowDialog("조회할 데이터가 없습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                }
            }
            catch (Exception ex)
            {
                ShowDialog(ex.Message, DC00_WinForm.DialogForm.DialogType.OK);
            }
            finally
            {
                helper.Close();
            }
        }

        public override void DoNew()
        {

        }

        public override void DoDelete()
        {

        }

        public override void DoSave()
        {
        }

        private void btnWorker_Click(object sender, EventArgs e)
        {
            // 작업자 등록 시작
            if (grid1.Rows.Count == 0) return;
            if (grid1.ActiveRow == null)
            {
                ShowDialog("작업지시를 선택 후 진행 하세요.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }

            string sWorkId = txtWorkerId.Text.ToString();
            if (sWorkId == "")
            {
                ShowDialog("작업자를 선택후 진행하세요.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }
            // DB 에 등록하기 위한 변수 지정
            string sOrederNo = grid1.ActiveRow.Cells["ORDERNO"].Value.ToString();
            string sWOrkcentercode = grid1.ActiveRow.Cells["WORKCENTERCODE"].Value.ToString();

            DBHelper helper = new DBHelper("", true);
            try
            {
                helper.ExecuteNoneQuery("14PP_ActureOutput_I2", CommandType.StoredProcedure,
                                        helper.CreateParameter("PLANTCODE",      "1000",          DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("WORKER",         sWorkId,         DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("ORDERNO",        sOrederNo,       DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("WORKCENTERCODE", sWOrkcentercode, DbType.String, ParameterDirection.Input)
                                        );
                if (helper.RSCODE == "S")
                {
                    helper.Commit();
                    ShowDialog(helper.RSMSG, DC00_WinForm.DialogForm.DialogType.OK);
                }
                else
                {
                    helper.Rollback();
                    ShowDialog(helper.RSMSG, DC00_WinForm.DialogForm.DialogType.OK);
                }
            }
            catch (Exception ex)
            {
                helper.Rollback();
            }
            finally
            {
                helper.Close();
            }
        }

        private void btnLotIn_Click(object sender, EventArgs e)
        {
            // LOT 투입
            if (this.grid1.ActiveRow == null) return;
            DBHelper helper = new DBHelper("", true);
            try
            {
                string sItemcode         = Convert.ToString(grid1.ActiveRow.Cells["ITEMCODE"].Value);
                string sLotno            = Convert.ToString(txtInLotNo.Text);
                string sWorkercenterCode = Convert.ToString(grid1.ActiveRow.Cells["WORKCENTERCODE"].Value);

                string sOrderno          = Convert.ToString(grid1.ActiveRow.Cells["ORDERNO"].Value);
                string sUnitCode         = Convert.ToString(grid1.ActiveRow.Cells["UNITCODE"].Value);
                string sInFlag           = Convert.ToString(btnLotIn.Text);
                string sWorker           = Convert.ToString(grid1.ActiveRow.Cells["WORKER"].Value);
                if (sInFlag == "투입")
                {
                    sInFlag = "IN";
                }
                else sInFlag = "OUT";

                helper.ExecuteNoneQuery("14PP_ActureOutput_I1", CommandType.StoredProcedure,
                                        helper.CreateParameter("PLANTCODE",      "1000",            DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("ITEMCODE",       sItemcode,         DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("LOTNO",          sLotno,            DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("WORKCENTERCODE", sWorkercenterCode, DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("ORDERNO",        sOrderno,          DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("UNITCODE",       sUnitCode,         DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("INFLAG",         sInFlag,           DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("MAKER",          sWorker,           DbType.String, ParameterDirection.Input)
                                        );
                if (helper.RSCODE == "S")
                {
                    helper.Commit();
                    ShowDialog(helper.RSMSG, DC00_WinForm.DialogForm.DialogType.OK);
                    DoInquire(); 
                }
                else
                {
                    helper.Rollback();
                    ShowDialog(helper.RSMSG, DC00_WinForm.DialogForm.DialogType.OK);
                }

                helper.Commit();
            }
            catch (Exception ex)
            {
                helper.Rollback();
                ShowDialog(ex.ToString(), DC00_WinForm.DialogForm.DialogType.OK);
            }
            finally
            {
                helper.Close();
            }


        }
        private void grid1_AfterRowActivate_1(object sender, EventArgs e)
        {
            if (Convert.ToString(this.grid1.ActiveRow.Cells["WORKSTATUSCODE"].Value) == "R")
            {
                btnNonStop.Text = "비가동";
            }
            else btnNonStop.Text = "가동";

            string sMatLotno = Convert.ToString(grid1.ActiveRow.Cells["MATLOTNO"].Value);
            if (sMatLotno != "")
            {
                txtInLotNo.Text = sMatLotno;
                btnLotIn.Text = "투입취소";
            }
            else
            {
                txtInLotNo.Text = "";
                btnLotIn.Text = "투입";
            }
            txtWorkerId.Text   = Convert.ToString(grid1.ActiveRow.Cells["WORKER"].Value);
            txtWorkerName.Text = Convert.ToString(grid1.ActiveRow.Cells["WORKERNAME"].Value);
        }

        private void btnNonStop_Click(object sender, EventArgs e)
        {
            // 가동/비가동 등록
            DBHelper helper = new DBHelper("", true);
            try
            {
                string sStatus = "R";
                if (btnNonStop.Text == "비가동") sStatus = "S";
                helper.ExecuteNoneQuery("14PP_ActureOutput_U1", CommandType.StoredProcedure
                , helper.CreateParameter("PLANTCODE",           "1000",                                                               DbType.String, ParameterDirection.Input)
                , helper.CreateParameter("WORKCENTERCODE",      Convert.ToString(this.grid1.ActiveRow.Cells["WORKCENTERCODE"].Value), DbType.String, ParameterDirection.Input)
                , helper.CreateParameter("ORDERNO",             Convert.ToString(this.grid1.ActiveRow.Cells["ORDERNO"].Value),        DbType.String, ParameterDirection.Input)
                , helper.CreateParameter("ITEMCODE",            Convert.ToString(this.grid1.ActiveRow.Cells["ITEMCODE"].Value),       DbType.String, ParameterDirection.Input)
                , helper.CreateParameter("UNITCODE",            Convert.ToString(this.grid1.ActiveRow.Cells["UNITCODE"].Value),       DbType.String, ParameterDirection.Input)
                , helper.CreateParameter("STATUS",              sStatus,                                                              DbType.String, ParameterDirection.Input)
                );  //앞부분은 맨위의 그리드 세팅과 동일하면 상관 없음. 뒤에 있는 내용은 실제 열에 있는것이어야만합니다.
                if(helper.RSCODE == "S")
                {
                    helper.Commit();
                    ShowDialog("정상적으로 등록되었습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                    DoInquire();
                }
                else
                {
                    helper.Rollback();
                    ShowDialog("데이터 등록 중 오류가 발생하였습니다."+ helper.RSMSG, DC00_WinForm.DialogForm.DialogType.OK);
                }
            }
            catch(Exception ex)
            {
                helper.Rollback();
                ShowDialog(ex.ToString());
            }
            finally
            {
                helper.Close();
            }
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            // 생산 실적 등록
            if(this.grid1.ActiveRow == null) // 벨류체크
            {
                ShowDialog("작업지시를 선택 하세요.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }
            double dProdQty   = 0;    // 누적 양품 수량
            double dErrorQty  = 0;    // 누적 불량 수량
            double dTProdQty  = 0;    // 입력 양품 수량
            double dTErrorQty = 0;    // 입력 불량 수량
            double dOrderQty  = 0;    // 작업 지시 수량
            double dInQty     = 0;    // 투입 LOT 잔량

            string sProdQty = Convert.ToString(this.grid1.ActiveRow.Cells["PRODQTY"].Value).Replace(",", "");
            double.TryParse(sProdQty, out dProdQty);
            //try
            //{
            //dProdQty = Convert.ToDouble(sProdQty); 랑 같습니다. 다만 string 값을 만약 Double값으로 변경이 안되버리면 안좋음.
            //}
            //catch 트라이 케치형태로 이런 식으로 사용 가능합니다.
            //{
            //  
            //}
            string sBadQty = Convert.ToString(this.grid1.ActiveRow.Cells["BADQTY"].Value).Replace(",", "");
            double.TryParse(sBadQty, out dErrorQty);

            string sTProdQty = Convert.ToString(txtProduct.Text);
            double.TryParse(sTProdQty, out dTProdQty);

            string sTBadQty = Convert.ToString(txtBad.Text);
            double.TryParse(sTBadQty, out dTErrorQty);

            string sOrderQty = Convert.ToString(this.grid1.ActiveRow.Cells["PLANQTY"].Value).Replace(",", "");
            double.TryParse(sOrderQty, out dOrderQty);

            string sInQty = Convert.ToString(this.grid1.ActiveRow.Cells["COMPONENTQTY"].Value).Replace(",", "");
            double.TryParse(sInQty, out dInQty);

            if(dInQty == 0)
            {
                ShowDialog("투입한 LOT가 존재하지 않습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }
            if((dTProdQty + dTErrorQty) == 0) //아무것도 안넣고 생산실적 넣은 경우
            {
                ShowDialog("실적 수량을 입력하세요.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }
            if (dOrderQty < (dProdQty + dErrorQty) + (dTProdQty + dTErrorQty)) //아무것도 안넣고 생산실적 넣은 경우
            {
                ShowDialog("생산수량 및 불량 수량의 합계가 지시수량보다 많습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }

            DBHelper helper = new DBHelper("", true);
            try
            {
                helper.ExecuteNoneQuery("14PP_ActureOutput_U2", CommandType.StoredProcedure
                                                                   , helper.CreateParameter("PLANTCODE",        "1000",                                                               DbType.String, ParameterDirection.Input)
                                                                   , helper.CreateParameter("WORKCENTERCODE",   Convert.ToString(this.grid1.ActiveRow.Cells["WORKCENTERCODE"].Value), DbType.String, ParameterDirection.Input)
                                                                   , helper.CreateParameter("ORDERNO",          Convert.ToString(this.grid1.ActiveRow.Cells["ORDERNO"].Value),        DbType.String, ParameterDirection.Input)
                                                                   , helper.CreateParameter("ITEMCODE",         Convert.ToString(this.grid1.ActiveRow.Cells["ITEMCODE"].Value),       DbType.String, ParameterDirection.Input)
                                                                   , helper.CreateParameter("UNITCODE",         Convert.ToString(this.grid1.ActiveRow.Cells["UNITCODE"].Value),       DbType.String, ParameterDirection.Input)
                                                                   , helper.CreateParameter("PRODQTY",          dTProdQty,                                                            DbType.String, ParameterDirection.Input)
                                                                   , helper.CreateParameter("ERRORQTY",         dTErrorQty,                                                           DbType.String, ParameterDirection.Input)
                                                                   , helper.CreateParameter("MATLOTNO",         Convert.ToString(this.grid1.ActiveRow.Cells["MATLOTNO"].Value),       DbType.String, ParameterDirection.Input)
                                                                   , helper.CreateParameter("CITEMCODE",        Convert.ToString(this.grid1.ActiveRow.Cells["COMPONENT"].Value),      DbType.String, ParameterDirection.Input)
                                                                   , helper.CreateParameter("CUNITCODE",        Convert.ToString(this.grid1.ActiveRow.Cells["CUNITCODE"].Value),      DbType.String, ParameterDirection.Input)

                                                                   );
                if (helper.RSCODE != "S")
                {
                    helper.Rollback();
                    ShowDialog(helper.RSMSG);
                    return;
                }
                helper.Commit();
                ShowDialog("생산실적 등록을 완료 하였습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                DoInquire();
                //txtInLotNo.Text = ""; 안써도 되니까 뺌.
                txtProduct.Text = "";
                txtBad.Text = "";
            }
            catch(Exception ex)
            {
                helper.Rollback();
                ShowDialog(ex.ToString(), DC00_WinForm.DialogForm.DialogType.OK);
            }
            finally
            {
                helper.Close();
            }

        }

        private void btnOrderClose_Click(object sender, EventArgs e)
        {
            //작업지시 종료
            if (grid1.Rows.Count == 0) return;
            if (grid1.ActiveRow == null) return;
            if (Convert.ToString(grid1.ActiveRow.Cells["MATLOTNO"].Value) != "")
            {
                ShowDialog("LOT 투입 취소 후 진행하세요.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }
            //가동 일 경우 종료 안되도록 확인
            if (Convert.ToString(grid1.ActiveRow.Cells["WORKSTATUSCODE"].Value) == "R")
            {
                ShowDialog("비가동 등록 후 진행하세요", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }
            DBHelper helper = new DBHelper("", true);
            try 
            {
                helper.ExecuteNoneQuery("14PP_ActureOutput_U3", CommandType.StoredProcedure
                                                                    , helper.CreateParameter("PLANTCODE", "1000", DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("WORKCENTERCODE", Convert.ToString(this.grid1.ActiveRow.Cells["WORKCENTERCODE"].Value), DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("ORDERNO", Convert.ToString(this.grid1.ActiveRow.Cells["ORDERNO"].Value), DbType.String, ParameterDirection.Input)
                                                                    );
                if (helper.RSCODE != "S")
                {
                    helper.Rollback();
                    ShowDialog(helper.RSMSG);
                    return;
                }
                helper.Commit();
                ShowDialog("상태 등록을 완료 하였습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                DoInquire();
                txtInLotNo.Text = "";
                txtProduct.Text = "";
                txtBad.Text = "";
            }
            catch (Exception ex)
            {
                helper.Rollback();
                ShowDialog(ex.ToString(), DC00_WinForm.DialogForm.DialogType.OK);
            }
            finally
            {

            }
        }
    }
}