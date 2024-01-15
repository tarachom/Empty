

/*     
        Користувачі.cs
        Список
*/

using AccountingSoftware;
using StorageAndTrade_1_0.Довідники;
using ТабличніСписки = StorageAndTrade_1_0.Довідники.ТабличніСписки;

namespace StorageAndTrade
{
    public class Користувачі : ДовідникЖурнал
    {
        public Користувачі() : base()
        {
            ТабличніСписки.Користувачі_Записи.AddColumns(TreeViewGrid);
        }

        #region Override

        public override async ValueTask LoadRecords()
        {
            ТабличніСписки.Користувачі_Записи.SelectPointerItem = SelectPointerItem;
            ТабличніСписки.Користувачі_Записи.DirectoryPointerItem = DirectoryPointerItem;

            ТабличніСписки.Користувачі_Записи.ОчиститиВідбір(TreeViewGrid);

            await ТабличніСписки.Користувачі_Записи.LoadRecords(TreeViewGrid);

            if (ТабличніСписки.Користувачі_Записи.SelectPath != null)
                TreeViewGrid.SetCursor(ТабличніСписки.Користувачі_Записи.SelectPath, TreeViewGrid.Columns[0], false);

            TreeViewGrid.GrabFocus();
        }

        protected override async ValueTask LoadRecords_OnSearch(string searchText)
        {
            searchText = searchText.ToLower().Trim();

            if (searchText.Length < 1)
                return;

            searchText = "%" + searchText.Replace(" ", "%") + "%";

            //Відбори
            ТабличніСписки.Користувачі_Записи.ДодатиВідбір(TreeViewGrid, Користувачі_ВідбориДляПошуку.Відбори(searchText), true);

            await ТабличніСписки.Користувачі_Записи.LoadRecords(TreeViewGrid);

            if (ТабличніСписки.Користувачі_Записи.FirstPath != null)
                TreeViewGrid.SetCursor(ТабличніСписки.Користувачі_Записи.FirstPath, TreeViewGrid.Columns[0], false);
        }

        protected override async void OpenPageElement(bool IsNew, UnigueID? unigueID = null)
        {
            if (IsNew)
            {
                Program.GeneralForm?.CreateNotebookPage($"{Користувачі_Const.FULLNAME} *", () =>
                {
                    Користувачі_Елемент page = new Користувачі_Елемент
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
                Користувачі_Objest Користувачі_Objest = new Користувачі_Objest();
                if (await Користувачі_Objest.Read(unigueID))
                {
                    Program.GeneralForm?.CreateNotebookPage($"{Користувачі_Objest.Назва}", () =>
                    {
                        Користувачі_Елемент page = new Користувачі_Елемент
                        {
                            CallBack_LoadRecords = CallBack_LoadRecords,
                            IsNew = false,
                            Користувачі_Objest = Користувачі_Objest,
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
            Користувачі_Objest Користувачі_Objest = new Користувачі_Objest();
            if (await Користувачі_Objest.Read(unigueID))
                await Користувачі_Objest.SetDeletionLabel(!Користувачі_Objest.DeletionLabel);
            else
                Message.Error(Program.GeneralForm, "Не вдалось прочитати!");
        }

        protected override async ValueTask<UnigueID?> Copy(UnigueID unigueID)
        {
            Користувачі_Objest Користувачі_Objest = new Користувачі_Objest();
            if (await Користувачі_Objest.Read(unigueID))
            {
                Користувачі_Objest Користувачі_Objest_Новий = await Користувачі_Objest.Copy(true);
                await Користувачі_Objest_Новий.Save();

                return Користувачі_Objest_Новий.UnigueID;
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
