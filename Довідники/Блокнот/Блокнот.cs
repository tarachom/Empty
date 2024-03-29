

/*     
        Блокнот.cs
        Список
*/

using AccountingSoftware;
using StorageAndTrade_1_0.Довідники;
using ТабличніСписки = StorageAndTrade_1_0.Довідники.ТабличніСписки;

namespace StorageAndTrade
{
    public class Блокнот : ДовідникЖурнал
    {
        public Блокнот() : base()
        {
            ТабличніСписки.Блокнот_Записи.AddColumns(TreeViewGrid);
        }

        #region Override

        public override async ValueTask LoadRecords()
        {
            ТабличніСписки.Блокнот_Записи.SelectPointerItem = SelectPointerItem;
            ТабличніСписки.Блокнот_Записи.DirectoryPointerItem = DirectoryPointerItem;

            ТабличніСписки.Блокнот_Записи.ОчиститиВідбір(TreeViewGrid);

            await ТабличніСписки.Блокнот_Записи.LoadRecords(TreeViewGrid);

            if (ТабличніСписки.Блокнот_Записи.SelectPath != null)
                TreeViewGrid.SetCursor(ТабличніСписки.Блокнот_Записи.SelectPath, TreeViewGrid.Columns[0], false);

            TreeViewGrid.GrabFocus();
        }

        protected override async ValueTask LoadRecords_OnSearch(string searchText)
        {
            searchText = searchText.ToLower().Trim();

            if (searchText.Length < 1)
                return;

            searchText = "%" + searchText.Replace(" ", "%") + "%";

            //Відбори
            ТабличніСписки.Блокнот_Записи.ДодатиВідбір(TreeViewGrid, Блокнот_ВідбориДляПошуку.Відбори(searchText), true);

            await ТабличніСписки.Блокнот_Записи.LoadRecords(TreeViewGrid);

            if (ТабличніСписки.Блокнот_Записи.FirstPath != null)
                TreeViewGrid.SetCursor(ТабличніСписки.Блокнот_Записи.FirstPath, TreeViewGrid.Columns[0], false);
        }

        protected override async void OpenPageElement(bool IsNew, UnigueID? unigueID = null)
        {
            if (IsNew)
            {
                Program.GeneralForm?.CreateNotebookPage($"{Блокнот_Const.FULLNAME} *", () =>
                {
                    Блокнот_Елемент page = new Блокнот_Елемент
                    {
                        CallBack_LoadRecords = CallBack_LoadRecords,
                        IsNew = true
                    };

                    page.SetValue();

                    return page;
                }, true);
            }
            else if (unigueID != null)
            {
                Блокнот_Objest Блокнот_Objest = new Блокнот_Objest();
                if (await Блокнот_Objest.Read(unigueID))
                {
                    Program.GeneralForm?.CreateNotebookPage($"{Блокнот_Objest.Назва}", () =>
                    {
                        Блокнот_Елемент page = new Блокнот_Елемент
                        {
                            CallBack_LoadRecords = CallBack_LoadRecords,
                            IsNew = false,
                            Блокнот_Objest = Блокнот_Objest,
                        };

                        page.SetValue();

                        return page;
                    }, true);
                }
                else
                    Message.Error(Program.GeneralForm, "Не вдалось прочитати!");
            }
        }

        protected override async ValueTask SetDeletionLabel(UnigueID unigueID)
        {
            Блокнот_Objest Блокнот_Objest = new Блокнот_Objest();
            if (await Блокнот_Objest.Read(unigueID))
                await Блокнот_Objest.SetDeletionLabel(!Блокнот_Objest.DeletionLabel);
            else
                Message.Error(Program.GeneralForm, "Не вдалось прочитати!");
        }

        protected override async ValueTask<UnigueID?> Copy(UnigueID unigueID)
        {
            Блокнот_Objest Блокнот_Objest = new Блокнот_Objest();
            if (await Блокнот_Objest.Read(unigueID))
            {
                Блокнот_Objest Блокнот_Objest_Новий = await Блокнот_Objest.Copy(true);
                await Блокнот_Objest_Новий.Save();

                return Блокнот_Objest_Новий.UnigueID;
            }
            else
            {
                Message.Error(Program.GeneralForm, "Не вдалось прочитати!");
                return null;
            }
        }

        #endregion
    }
}
