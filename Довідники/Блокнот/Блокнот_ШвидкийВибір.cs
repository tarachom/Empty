

/*     
        Блокнот_ШвидкийВибір.cs
        ШвидкийВибір
*/

using Gtk;

using AccountingSoftware;
using StorageAndTrade_1_0.Довідники;
using ТабличніСписки = StorageAndTrade_1_0.Довідники.ТабличніСписки;

namespace StorageAndTrade
{
    class Блокнот_ШвидкийВибір : ДовідникШвидкийВибір
    {
        public Блокнот_ШвидкийВибір() : base()
        {
            ТабличніСписки.Блокнот_ЗаписиШвидкийВибір.AddColumns(TreeViewGrid);

            //Сторінка
            {
                LinkButton linkPage = new LinkButton($" {Блокнот_Const.FULLNAME}") { Halign = Align.Start, Image = new Image(AppContext.BaseDirectory + "images/doc.png"), AlwaysShowImage = true };
                linkPage.Clicked += async (object? sender, EventArgs args) =>
                {
                    Блокнот page = new Блокнот()
                    {
                        DirectoryPointerItem = DirectoryPointerItem,
                        CallBack_OnSelectPointer = CallBack_OnSelectPointer
                    };

                    Program.GeneralForm?.CreateNotebookPage($"Вибір - {Блокнот_Const.FULLNAME}", () => { return page; }, true);

                    await page.LoadRecords();
                };

                HBoxTop.PackStart(linkPage, false, false, 10);
            }

            //Новий
            {
                LinkButton linkNew = new LinkButton("Новий");
                linkNew.Clicked += (object? sender, EventArgs args) =>
                {
                    Блокнот_Елемент page = new Блокнот_Елемент
                    {
                        IsNew = true,
                        CallBack_OnSelectPointer = CallBack_OnSelectPointer
                    };

                    Program.GeneralForm?.CreateNotebookPage($"{Блокнот_Const.FULLNAME} *", () => { return page; }, true);

                    page.SetValue();
                };

                HBoxTop.PackStart(linkNew, false, false, 0);
            }
        }

        public override async ValueTask LoadRecords()
        {
            ТабличніСписки.Блокнот_ЗаписиШвидкийВибір.DirectoryPointerItem = DirectoryPointerItem;

            ТабличніСписки.Блокнот_ЗаписиШвидкийВибір.ОчиститиВідбір(TreeViewGrid);

            await ТабличніСписки.Блокнот_ЗаписиШвидкийВибір.LoadRecords(TreeViewGrid);

            if (ТабличніСписки.Блокнот_ЗаписиШвидкийВибір.SelectPath != null)
                TreeViewGrid.SetCursor(ТабличніСписки.Блокнот_ЗаписиШвидкийВибір.SelectPath, TreeViewGrid.Columns[0], false);
        }

        protected override async ValueTask LoadRecords_OnSearch(string searchText)
        {
            searchText = searchText.ToLower().Trim();

            if (searchText.Length < 1)
                return;

            searchText = "%" + searchText.Replace(" ", "%") + "%";

            //Відбори
            ТабличніСписки.Блокнот_Записи.ДодатиВідбір(TreeViewGrid, Блокнот_ВідбориДляПошуку.Відбори(searchText), true);

            await ТабличніСписки.Блокнот_ЗаписиШвидкийВибір.LoadRecords(TreeViewGrid);
        }
    }
}
