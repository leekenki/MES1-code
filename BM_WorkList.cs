using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using DC00_assm;

namespace KFQS_Form
{
    public partial class BM_WorkList : DC00_WinForm.BaseMDIChildForm
    {
        //그리드를 셋팅 할 수 있도록 도와주는 함수 클레스
        UltraGridUtil _GridUtil = new UltraGridUtil();
        //공장 변수 입력
        //Pirvate sPlantCode = Logininfol.
        public BM_WorkList()
        {
            InitializeComponent();///SDFSDFDSF
        }

        private void BM_WorkList_Load(object sender, EventArgs e)
        {
            //그리드를 셋팅한다(f4, 이벤트, load 더블클릭)
            try
            {
                _GridUtil.InitializeGrid(this.grid1, false, true, false, "", false);
                _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",   "공장", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "WORKERID",    "작업자ID", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "WORKERNAME",  "작업자 명", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "BANCODE",     "작업반", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "GRPID",       "그룹", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "DEPTCODE",    "부서", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "PHONENO",       "연락처", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "INDATE",      "입사일", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "OUTDATE",     "퇴사일", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "USEFLAG",     "사용여부", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE",    "등록일시", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "MAKER",       "등록자", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "EDITDATE",    "수정일시", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "EDITOR",      "수정자", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);

                //셋팅 내역 그리드와 바인딩
                _GridUtil.SetInitUltraGridBind(grid1); //셋팅 내역 그리드와 바인딩

                Common _Common = new Common();
                DataTable dtTemp = new DataTable();
                //플렌트코드 기준정보를 가져와서 데이터 테이블에 추가.

                dtTemp = _Common.Standard_CODE("PLANTCODE");
                //데이터 테이블에 있는 데이터를 해당 콤보박스에 추가.
                Common.FillComboboxMaster(this.cboPlantCode_H, dtTemp, 
                                            dtTemp.Columns["CODE_ID"].ColumnName,
                                            dtTemp.Columns["CODE_NAME"].ColumnName,
                                            "ALL","");
                UltraGridUtil.SetComboUltraGrid(this.grid1,  "PLANTCODE", dtTemp, "CODE_ID", "CODE_NAME");  //콤보박스 형태로 바인딩시킴

                dtTemp = _Common.Standard_CODE("BANCODE");
                //데이터 테이블에 있는 데이터를 해당 콤보박스에 추가.
                Common.FillComboboxMaster(this.cboBanCode_H, dtTemp,
                                            dtTemp.Columns["CODE_ID"].ColumnName,
                                            dtTemp.Columns["CODE_NAME"].ColumnName,
                                            "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "BANCODE", dtTemp, "CODE_ID", "CODE_NAME");
                dtTemp = _Common.Standard_CODE("USEFLAG");
                //데이터 테이블에 있는 데이터를 해당 콤보박스에 추가.
                Common.FillComboboxMaster(this.cboUserFlag_H, dtTemp,
                                            dtTemp.Columns["CODE_ID"].ColumnName,
                                            dtTemp.Columns["CODE_NAME"].ColumnName,
                                            "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "USEFLAG", dtTemp, "CODE_ID", "CODE_NAME");

                //부서
                dtTemp = _Common.Standard_CODE("DEPTCODE");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "DEPTCODE", dtTemp, "CODE_ID", "CODE_NAME");

                //그룹
                dtTemp = _Common.Standard_CODE("GRPID");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "GRPID", dtTemp, "CODE_ID", "CODE_NAME");

            }
            catch(Exception ex)
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
                string sPlantcode = cboPlantCode_H.Value.ToString();
                string sWorkerid = txtWorkerId_H.Text.ToString();
                string sWorkerName = txtWorkerName_H.Text.ToString();
                string sBanCode = cboBanCode_H.Value.ToString();
                string sUseflag = cboUserFlag_H.Value.ToString();

                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("14BM_WorkList_S1", CommandType.StoredProcedure 
                                        ,helper.CreateParameter("PLANTCODE", sPlantcode, DbType.String, ParameterDirection.Input)
                                        ,helper.CreateParameter("WORKERID", sWorkerid, DbType.String, ParameterDirection.Input)
                                        ,helper.CreateParameter("WORKERNAME", sWorkerName, DbType.String, ParameterDirection.Input)
                                        ,helper.CreateParameter("BANCODE", sBanCode, DbType.String, ParameterDirection.Input)
                                        ,helper.CreateParameter("USEFLAG", sUseflag, DbType.String, ParameterDirection.Input));
                this.ClosePrgForm(); //이건 별도의 프로그램 안에 있는 것입니다.
                if(dtTemp.Rows.Count > 0)
                {
                    grid1.DataSource = dtTemp;
                    grid1.DataBinds(dtTemp);
                }
                else
                {
                    //조회하기 이전에 데이터가 남으면 안되기 때문에, 초기화 시켜줌
                    _GridUtil.Grid_Clear(grid1);
                    ShowDialog("조회할 데이터가 없습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                }

            }
            catch(Exception ex)
            {

            }
            finally
            {
                helper.Close();            
            }
        }
        public override void DoNew()
        {
            base.DoNew();
            this.grid1.InsertRow();

            this.grid1.ActiveRow.Cells["PLANTCODE"].Value  = "1000";
            this.grid1.ActiveRow.Cells["GRPID"].Value      = "SW";
            this.grid1.ActiveRow.Cells["USEFLAG"].Value    = "Y";
            this.grid1.ActiveRow.Cells["INDATE"].Value     = DateTime.Now.ToString("yyyy-MM-dd");

            grid1.ActiveRow.Cells["MAKER"].Activation      = Activation.NoEdit;
            grid1.ActiveRow.Cells["MAKEDATE"].Activation   = Activation.NoEdit;
            grid1.ActiveRow.Cells["EDITDATE"].Activation   = Activation.NoEdit;
            grid1.ActiveRow.Cells["EDITOR"].Activation     = Activation.NoEdit;
        }
        public override void DoDelete() 
        {
            base.DoDelete();
            this.grid1.DeleteRow(); 
            


        }
        public override void DoSave() //그리드에 있는걸 다 가져와서 변경된걸 for문돌림
        {
            base.DoSave();
            DataTable dtTemp = new DataTable();
            dtTemp = grid1.chkChange();  // 바뀐정보의 행을 받아오는 함수임 이건
            if (dtTemp.Rows.Count == 0) return;

            DBHelper helper = new DBHelper("", true);  //""꼭 넣어야함. 앞으론 이렇게 걸으시오

            //해당내역을 저장하시겠습니까? (DELETE에 있던걸 가져와서 여기에 넣음(
            if (ShowDialog("해당 사항을 저장 하시겠습니까?", DC00_WinForm.DialogForm.DialogType.YESNO)
                == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            try
            {
                  
                foreach (DataRow drrow in dtTemp.Rows)
                {
                    
                    switch(drrow.RowState) //drrow는 dt소스에서 바뀐 행렬만 쪽쪽쪽 빼온것.
                    {
                        case DataRowState.Deleted:
                            drrow.RejectChanges();
                            helper.ExecuteNoneQuery("14BM_WorkList_D1", CommandType.StoredProcedure,
                            helper.CreateParameter("PLANTCODE", Convert.ToString(drrow["PLANTCODE"]),
                                   DbType.String, ParameterDirection.Input),
                            helper.CreateParameter("WORKERID", Convert.ToString(drrow["WORKERID"]),
                                   DbType.String, ParameterDirection.Input));
                            break;
                        case DataRowState.Added:
                            if (Convert.ToString(drrow["WORKERID"]) == string.Empty)
                            {
                                this.ClosePrgForm();
                                this.ShowDialog("작업자 ID를 입력하세요.", DC00_WinForm.DialogForm.DialogType.OK);
                                return;
                            }
                            helper.ExecuteNoneQuery("14BM_WorkList_I1"
                           , CommandType.StoredProcedure
                           , helper.CreateParameter("PLANTCODE",    Convert.ToString(drrow["PLANTCODE"]),      DbType.String, ParameterDirection.Input)
                           , helper.CreateParameter("WORKERID",     Convert.ToString(drrow["WORKERID"]),       DbType.String, ParameterDirection.Input)
                           , helper.CreateParameter("WORKERNAME",   Convert.ToString(drrow["WORKERNAME"]),     DbType.String, ParameterDirection.Input)
                           , helper.CreateParameter("GRPID",        Convert.ToString(drrow["GRPID"]),          DbType.String, ParameterDirection.Input)
                           , helper.CreateParameter("DEPTCODE",     Convert.ToString(drrow["DEPTCODE"]),       DbType.String, ParameterDirection.Input)
                           //, helper.CreateParameter("PDALOGINFLAG", Convert.ToString(drRow["PDALOGINFLAG"]), DbType.String, ParameterDirection.Input)
                           , helper.CreateParameter("BANCODE",      Convert.ToString(drrow["BANCODE"]),        DbType.String, ParameterDirection.Input)
                           , helper.CreateParameter("USEFLAG",      Convert.ToString(drrow["USEFLAG"]),        DbType.String, ParameterDirection.Input)
                           , helper.CreateParameter("PHONENO",      Convert.ToString(drrow["PHONENO"]),        DbType.String, ParameterDirection.Input)
                           , helper.CreateParameter("INDATE",       Convert.ToString(drrow["INDATE"]),         DbType.String, ParameterDirection.Input)
                           , helper.CreateParameter("OUTDATE",      Convert.ToString(drrow["OUTDATE"]),        DbType.String, ParameterDirection.Input)
                           , helper.CreateParameter("MAKER",        LoginInfo.UserID,                          DbType.String, ParameterDirection.Input)
                           //, helper.CreateParameter("SPCFLAG",      Convert.ToString(drRow["SPCFLAG"]),      DbType.String, ParameterDirection.Input)
                           //, helper.CreateParameter("PATROLFLAG",      Convert.ToString(drRow["PATROLFLAG"]),      DbType.String, ParameterDirection.Input)
                           );
                            break;
                        case DataRowState.Modified:
                            helper.ExecuteNoneQuery("14BM_WorkList_U1"
                           , CommandType.StoredProcedure
                           , helper.CreateParameter("PLANTCODE",    Convert.ToString(drrow["PLANTCODE"]),      DbType.String, ParameterDirection.Input)
                           , helper.CreateParameter("WORKERID",     Convert.ToString(drrow["WORKERID"]),       DbType.String, ParameterDirection.Input)
                           , helper.CreateParameter("WORKERNAME",   Convert.ToString(drrow["WORKERNAME"]),     DbType.String, ParameterDirection.Input)
                           , helper.CreateParameter("GRPID",        Convert.ToString(drrow["GRPID"]),          DbType.String, ParameterDirection.Input)
                           , helper.CreateParameter("DEPTCODE",     Convert.ToString(drrow["DEPTCODE"]),       DbType.String, ParameterDirection.Input)
                           //, helper.CreateParameter("PDALOGINFLAG", Convert.ToString(drRow["PDALOGINFLAG"]), DbType.String, ParameterDirection.Input)
                           , helper.CreateParameter("BANCODE",      Convert.ToString(drrow["BANCODE"]),        DbType.String, ParameterDirection.Input)
                           , helper.CreateParameter("USEFLAG",      Convert.ToString(drrow["USEFLAG"]),        DbType.String, ParameterDirection.Input)
                           , helper.CreateParameter("PHONENO",      Convert.ToString(drrow["PHONENO"]),        DbType.String, ParameterDirection.Input)
                           , helper.CreateParameter("INDATE",       Convert.ToString(drrow["INDATE"]),         DbType.String, ParameterDirection.Input)
                           , helper.CreateParameter("OUTDATE",      Convert.ToString(drrow["OUTDATE"]),        DbType.String, ParameterDirection.Input)
                           , helper.CreateParameter("EDITOR",        LoginInfo.UserID,                          DbType.String, ParameterDirection.Input)
                           );
                           //, helper.CreateParameter("SPCFLAG",      Convert.ToString(drRow["SPCFLAG"]),      DbType.String, ParameterDirection.Input)
                           //, helper.CreateParameter("PATROLFLAG",      Convert.ToString(drRow["PATROLFLAG"]),      DbType.String, ParameterDirection.Input)
                            break;
                    }
                }
                if (helper.RSCODE == "S")
                {
                    helper.Commit();
                    this.ShowDialog("정상적으로 등록되었습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                    //showdialog 이 매세지 띄워주고 다른걸 막는겁니다.그래서 이 메세지 뜨면 커밋 안되는거지?
                    //그런고로 커밋이 위에 나와야합니다.
                    DoInquire(); //이럼에도 불구하고 사용할 수 있는 유일한 방법은 WITH(NOLOCK)입니다.
                }
            }
            catch(Exception ex)
            {
                helper.Rollback();  //트랜잭션 걸때는 롤벡, 클로즈 반드시 넣을 것.
            }
            finally
            {
                helper.Close();
            }
        }
    }
}
