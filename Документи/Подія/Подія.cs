

/*     
        Подія.cs
        Список
*/

using Gtk;

using AccountingSoftware;

using ТабличніСписки = StorageAndTrade_1_0.Документи.ТабличніСписки;
using StorageAndTrade_1_0.Документи;
using StorageAndTrade_1_0.Перелічення;

namespace StorageAndTrade
{
    public class Подія : ДокументЖурнал
    {
        public Подія() : base()
        {
            TreeViewGrid.Model = ТабличніСписки.Подія_Записи.Store;
            ТабличніСписки.Подія_Записи.AddColumns(TreeViewGrid);
        }

        #region Override

        public override void LoadRecords()
        {
            ТабличніСписки.Подія_Записи.SelectPointerItem = SelectPointerItem;
            ТабличніСписки.Подія_Записи.DocumentPointerItem = DocumentPointerItem;

            ТабличніСписки.Подія_Записи.LoadRecords();

            if (ТабличніСписки.Подія_Записи.SelectPath != null)
                TreeViewGrid.SetCursor(ТабличніСписки.Подія_Записи.SelectPath, TreeViewGrid.Columns[0], false);
            else if (ТабличніСписки.Подія_Записи.CurrentPath != null)
                TreeViewGrid.SetCursor(ТабличніСписки.Подія_Записи.CurrentPath, TreeViewGrid.Columns[0], false);

            TreeViewGrid.GrabFocus();
        }

        protected override void LoadRecords_OnSearch(string searchText)
        {
            searchText = searchText.ToLower().Trim();

            if (searchText.Length < 1)
                return;

            searchText = "%" + searchText.Replace(" ", "%") + "%";

            ТабличніСписки.Подія_Записи.Where.Clear();

            //Назва
            ТабличніСписки.Подія_Записи.Where.Add(
                new Where(Подія_Const.Назва, Comparison.LIKE, searchText) { FuncToField = "LOWER" });

            ТабличніСписки.Подія_Записи.LoadRecords();

            if (ТабличніСписки.Подія_Записи.FirstPath != null)
                TreeViewGrid.SetCursor(ТабличніСписки.Подія_Записи.FirstPath, TreeViewGrid.Columns[0], false);

            TreeViewGrid.GrabFocus();
        }

        protected override void OpenPageElement(bool IsNew, UnigueID? unigueID = null)
        {
            if (IsNew)
            {
                Program.GeneralForm?.CreateNotebookPage($"{Подія_Const.FULLNAME} *", () =>
                {
                    Подія_Елемент page = new Подія_Елемент
                    {
                        CallBack_LoadRecords = CallBack_LoadRecords,
                        IsNew = true
                    };

                    page.SetValue();

                    return page;
                });
            }
            else if (unigueID != null)
            {
                Подія_Objest Подія_Objest = new Подія_Objest();
                if (Подія_Objest.Read(unigueID))
                {
                    Program.GeneralForm?.CreateNotebookPage($"{Подія_Objest.Назва}", () =>
                    {
                        Подія_Елемент page = new Подія_Елемент
                        {
                            CallBack_LoadRecords = CallBack_LoadRecords,
                            IsNew = false,
                            Подія_Objest = Подія_Objest,
                        };

                        page.SetValue();

                        return page;
                    });
                }
                else
                    Message.Error(Program.GeneralForm, "Не вдалось прочитати!");
            }
        }

        protected override void SetDeletionLabel(UnigueID unigueID)
        {
            Подія_Objest Подія_Objest = new Подія_Objest();
            if (Подія_Objest.Read(unigueID))
                Подія_Objest.SetDeletionLabel(!Подія_Objest.DeletionLabel);
            else
                Message.Error(Program.GeneralForm, "Не вдалось прочитати!");
        }

        protected override UnigueID? Copy(UnigueID unigueID)
        {
            Подія_Objest Подія_Objest = new Подія_Objest();
            if (Подія_Objest.Read(unigueID))
            {
                Подія_Objest Подія_Objest_Новий = Подія_Objest.Copy(true);
                Подія_Objest_Новий.Save();
                
                return Подія_Objest_Новий.UnigueID;
            }
            else
            {
                Message.Error(Program.GeneralForm, "Не вдалось прочитати!");
                return null;
            }
        }

        protected override void PeriodWhereChanged()
        {
            ТабличніСписки.Подія_Записи.ДодатиВідбірПоПеріоду(Enum.Parse<ТипПеріодуДляЖурналівДокументів>(ComboBoxPeriodWhere.ActiveId));
            LoadRecords();
        }

        protected override void SpendTheDocument(UnigueID unigueID, bool spendDoc)
        {
            Подія_Pointer Подія_Pointer = new Подія_Pointer(unigueID);
            Подія_Objest? Подія_Objest = Подія_Pointer.GetDocumentObject(true);
            if (Подія_Objest == null) return;

            if (spendDoc)
            {
                if (!Подія_Objest.SpendTheDocument(Подія_Objest.ДатаДок))
                    ФункціїДляПовідомлень.ВідкритиТермінал();
            }
            else
                Подія_Objest.ClearSpendTheDocument();
        }

        protected override DocumentPointer? ReportSpendTheDocument(UnigueID unigueID)
        {
            return new Подія_Pointer(unigueID);
        }

        protected override void ExportXML(UnigueID unigueID)
        {
            string pathToSave = System.IO.Path.Combine(AppContext.BaseDirectory, $"{Подія_Const.FULLNAME}_{unigueID}.xml");
            Подія_Export.ToXmlFile(new Подія_Pointer(unigueID), pathToSave);
        }

        #endregion
    }
}
    