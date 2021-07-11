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
    public partial class MM_StockCheckRec : DC00_WinForm.BaseMDIChildForm
    {
        DataTable table = new DataTable();
        DataTable rtnDtTemp = new DataTable();
        UltraGridUtil _GridUtil = new UltraGridUtil();
        //공장 변수 입력
        string plantCode = LoginInfo.PlantCode;
        public MM_StockCheckRec()
        {
            InitializeComponent();
        }

        private void MM_StockCheckRec_Load(object sender, EventArgs e)
        {
            // 그리드를 셋팅한다.
            try
            {
                _GridUtil.InitializeGrid(this.grid1, false, true, false, "", false);
                _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE"    , "공장", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "LOTNO"        , "LOT 번호", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE"     , "품목", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME"     , "품명", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "CHECKSEQ"     , "NO", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "INQTY"        , "수량", true, GridColDataType_emu.VarChar, 60, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "UNITCODE"     , "단위", true, GridColDataType_emu.VarChar, 60, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "CUSTCODE"     , "거래처", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Center, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "CHECKCODE"    , "검사항목코드", true, GridColDataType_emu.VarChar, 100, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "CHECKNAME"    , "검사항목명", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "EACHCHECK"    , "개별판정", true, GridColDataType_emu.VarChar, 60, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "TOTALCHECK"   , "종합판정", true, GridColDataType_emu.VarChar, 60, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "REMARK"       , "비고", true, GridColDataType_emu.VarChar, 150, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "EDITOR"       ,  "수정자", true, GridColDataType_emu.VarChar, 110, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "EDITDATE"     , "수정일시", true, GridColDataType_emu.VarChar, 160, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "TESTDATE"     , "판정일시", true, GridColDataType_emu.DateTime24, 160, 130, Infragistics.Win.HAlign.Left, true, false);

                //셋팅 내역 그리드와 바인딩
                _GridUtil.SetInitUltraGridBind(grid1); //셋팅 내역 그리드와 바인딩



                Common _Common = new Common();
                DataTable dtTemp = new DataTable();
                // PLANTCODE 기준정보 가져와서 데이터 테이블에 추가.
                dtTemp = _Common.Standard_CODE("PLANTCODE");
                // 데이터 테이블에 있는 데이터를 해당 콤보박스에 추가.
                Common.FillComboboxMaster(this.cboPlantCode_H, dtTemp,
                                          dtTemp.Columns["CODE_ID"].ColumnName,
                                          dtTemp.Columns["CODE_NAME"].ColumnName,
                                          "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", dtTemp, "CODE_ID", "CODE_NAME");



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
                string sPlantCode = Convert.ToString(cboPlantCode_H.Value);
                string sStartDate = string.Format("{0:yyyy-MM-dd}", dtpStart.Value);
                string sEndDate = string.Format("{0:yyyy-MM-dd}", dtpEnd.Value);
                string sLOTNO = Convert.ToString(txtLOTNO.Text);



                DataTable dtTemp = new DataTable();
                rtnDtTemp = helper.FillTable("1JO_MM_StockCheckRec_S1", CommandType.StoredProcedure
                                          , helper.CreateParameter("PLANTCODE", sPlantCode, DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("STARTDATE", sStartDate, DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("ENDDATE", sEndDate, DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("LOTNO", sLOTNO, DbType.String, ParameterDirection.Input)
                                          );
                this.ClosePrgForm();



                this.grid1.DisplayLayout.Override.MergedCellContentArea = MergedCellContentArea.VirtualRect;
                this.grid1.DisplayLayout.Bands[0].Columns["CHECKSEQ"].MergedCellStyle = MergedCellStyle.Always;
                this.grid1.DisplayLayout.Bands[0].Columns["PLANTCODE"].MergedCellStyle = MergedCellStyle.Always;
                this.grid1.DisplayLayout.Bands[0].Columns["LOTNO"].MergedCellStyle = MergedCellStyle.Always;
                this.grid1.DisplayLayout.Bands[0].Columns["ITEMCODE"].MergedCellStyle = MergedCellStyle.Always;
                this.grid1.DisplayLayout.Bands[0].Columns["ITEMNAME"].MergedCellStyle = MergedCellStyle.Always;


                if (rtnDtTemp.Rows.Count != 0)
                {
                    //SUB-TOTAL  --어찌보면 알고리즘의 일부
                    DataTable dtSubTotal = rtnDtTemp.Clone(); //데이터 테이블의 서식을 복사해오겠습니다.

                    string sWorkerRow = Convert.ToString(rtnDtTemp.Rows[0]["CHECKSEQ"]);
                    string stotalcheck = Convert.ToString(rtnDtTemp.Rows[0]["TOTALCHECK"]);
                    double snum        = Convert.ToDouble(rtnDtTemp.Rows[0]["INQTY"]);
                    string SDATE = Convert.ToString(rtnDtTemp.Rows[0]["TESTDATE"]);

                    dtSubTotal.Rows.Add(new object[] { Convert.ToString(rtnDtTemp.Rows[0]["PLANTCODE"   ]),
                                                       Convert.ToString(rtnDtTemp.Rows[0]["LOTNO"  ]),
                                                       Convert.ToString(rtnDtTemp.Rows[0]["ITEMCODE"      ]),
                                                       Convert.ToString(rtnDtTemp.Rows[0]["ITEMNAME"   ]),
                                                       Convert.ToString(rtnDtTemp.Rows[0]["CHECKSEQ"   ]),
                                                       Convert.ToString(rtnDtTemp.Rows[0]["INQTY"      ]),
                                                       Convert.ToString(rtnDtTemp.Rows[0]["UNITCODE"   ]),
                                                       Convert.ToString(rtnDtTemp.Rows[0]["CUSTCODE"   ]),
                                                       Convert.ToString(rtnDtTemp.Rows[0]["CHECKCODE"  ]),
                                                       Convert.ToString(rtnDtTemp.Rows[0]["CHECKNAME"  ]),
                                                       Convert.ToString(rtnDtTemp.Rows[0]["EACHCHECK"  ]),
                                                       Convert.ToString(rtnDtTemp.Rows[0]["TOTALCHECK" ]),
                                                       Convert.ToString(rtnDtTemp.Rows[0]["REMARK"     ]),
                                                       Convert.ToString(rtnDtTemp.Rows[0]["EDITOR"     ]),
                                                       Convert.ToString(rtnDtTemp.Rows[0]["EDITDATE"   ]),
                                                       Convert.ToString(rtnDtTemp.Rows[0]["TESTDATE"   ]) });

                    //알고리즘적인 관점에서 이해해야 합니다.
                    for (int i = 1; i < rtnDtTemp.Rows.Count; i++)
                    {
                        if (sWorkerRow == Convert.ToString(rtnDtTemp.Rows[i]["CHECKSEQ"]))
                        {


                            dtSubTotal.Rows.Add(new object[] { Convert.ToString(rtnDtTemp.Rows[i]["PLANTCODE"   ]),
                                                       Convert.ToString(rtnDtTemp.Rows[i]["LOTNO"  ]),
                                                       Convert.ToString(rtnDtTemp.Rows[i]["ITEMCODE"   ]),
                                                       Convert.ToString(rtnDtTemp.Rows[i]["ITEMNAME"   ]),
                                                       Convert.ToString(rtnDtTemp.Rows[i]["CHECKSEQ"   ]),
                                                       Convert.ToString(rtnDtTemp.Rows[i]["INQTY"      ]),
                                                       Convert.ToString(rtnDtTemp.Rows[i]["UNITCODE"   ]),
                                                       Convert.ToString(rtnDtTemp.Rows[i]["CUSTCODE"   ]),
                                                       Convert.ToString(rtnDtTemp.Rows[i]["CHECKCODE"  ]),
                                                       Convert.ToString(rtnDtTemp.Rows[i]["CHECKNAME"  ]),
                                                       Convert.ToString(rtnDtTemp.Rows[i]["EACHCHECK"  ]),
                                                       Convert.ToString(rtnDtTemp.Rows[i]["TOTALCHECK" ]),
                                                       Convert.ToString(rtnDtTemp.Rows[i]["REMARK"     ]),
                                                       Convert.ToString(rtnDtTemp.Rows[i]["EDITOR"     ]),
                                                       Convert.ToString(rtnDtTemp.Rows[i]["EDITDATE"   ]),
                                                       Convert.ToString(rtnDtTemp.Rows[i]["TESTDATE"   ]) });
                            continue;
                        }
                        else
                        {
                            if (stotalcheck == "OK")
                            {
                                stotalcheck = "합격";
                            }
                            else
                            {
                                stotalcheck = "불합격";
                            }
                              dtSubTotal.Rows.Add(new object[] { "", "", "", "", "", null, "", "", "검사 결과", "", "", stotalcheck, "", "", "", null });



                            dtSubTotal.Rows.Add(new object[] { Convert.ToString(rtnDtTemp.Rows[i]["PLANTCODE"   ]),
                                                       Convert.ToString(rtnDtTemp.Rows[i]["LOTNO"  ]),
                                                       Convert.ToString(rtnDtTemp.Rows[i]["ITEMCODE"      ]),
                                                       Convert.ToString(rtnDtTemp.Rows[i]["ITEMNAME"   ]),
                                                       Convert.ToString(rtnDtTemp.Rows[i]["CHECKSEQ"   ]),
                                                       Convert.ToString(rtnDtTemp.Rows[i]["INQTY"      ]),
                                                       Convert.ToString(rtnDtTemp.Rows[i]["UNITCODE"   ]),
                                                       Convert.ToString(rtnDtTemp.Rows[i]["CUSTCODE"   ]),
                                                       Convert.ToString(rtnDtTemp.Rows[i]["CHECKCODE"  ]),
                                                       Convert.ToString(rtnDtTemp.Rows[i]["CHECKNAME"  ]),
                                                       Convert.ToString(rtnDtTemp.Rows[i]["EACHCHECK"  ]),
                                                       Convert.ToString(rtnDtTemp.Rows[i]["TOTALCHECK" ]),
                                                       Convert.ToString(rtnDtTemp.Rows[i]["REMARK"     ]),
                                                       Convert.ToString(rtnDtTemp.Rows[i]["EDITOR"     ]),
                                                       Convert.ToString(rtnDtTemp.Rows[i]["EDITDATE"   ]),
                                                       Convert.ToString(rtnDtTemp.Rows[i]["TESTDATE"   ]) });
                            sWorkerRow = Convert.ToString(rtnDtTemp.Rows[i]["CHECKSEQ"]);
                            stotalcheck = Convert.ToString(rtnDtTemp.Rows[i]["TOTALCHECK"]);

                        }

                    }
                    
                    if (stotalcheck == "OK")
                    {
                        stotalcheck = "합격";
                    }
                    else
                    {
                        stotalcheck = "불합격";
                    }
                    dtSubTotal.Rows.Add(new object[] { "", "", "", "", "", null, "", "", "검사 결과", "", "", stotalcheck, "", "", "", null });
                    this.grid1.DataSource = dtSubTotal;
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
            DataTable dt = new DataTable();

            dt = grid1.chkChange();
            if (dt == null)
                return;

            DBHelper helper = new DBHelper("", true);



            try
            {
                //base.DoSave();

                if (this.ShowDialog("C:Q00009") == System.Windows.Forms.DialogResult.Cancel)
                {
                    CancelProcess = true;
                    return;
                }


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["CHECKSEQ"]) == "0") continue;

                    helper.ExecuteNoneQuery("1JO_MM_StockCheckRec_U1"
                                            , CommandType.StoredProcedure
                                            , helper.CreateParameter("PLANTCODE", Convert.ToString(dt.Rows[i]["PLANTCODE"]), DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("CHECKCODE", Convert.ToString(dt.Rows[i]["CHECKCODE"]), DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("REMARK", Convert.ToString(dt.Rows[i]["REMARK"]), DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("CHECKSEQ", Convert.ToString(dt.Rows[i]["CHECKSEQ"]), DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("EDITOR", LoginInfo.UserID, DbType.String, ParameterDirection.Input)
                                            );

                    if (helper.RSCODE != "S")
                    {
                        this.ShowDialog(helper.RSMSG, DC00_WinForm.DialogForm.DialogType.OK);
                        helper.Rollback();
                        return;
                    }

                }

                helper.Commit();
                this.ShowDialog("데이터가 저장 되었습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                this.ClosePrgForm();
                DoInquire();
            }
            catch (Exception ex)
            {
                helper.Rollback();
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                helper.Close();
            }
        }   
    }
}