

/*     
        Подія_PointerControl.cs
        PointerControl
*/

using AccountingSoftware;
using StorageAndTrade_1_0.Документи;

namespace StorageAndTrade
{
    class Подія_PointerControl : PointerControl
    {
        public Подія_PointerControl()
        {
            pointer = new Подія_Pointer();
            WidthPresentation = 300;
            Caption = $"{Подія_Const.FULLNAME}:";
        }

        Подія_Pointer pointer;
        public Подія_Pointer Pointer
        {
            get
            {
                return pointer;
            }
            set
            {
                pointer = value;

                if (pointer != null)
                    Presentation = pointer.GetPresentation();
                else
                    Presentation = "";
            }
        }

        //Відбір по періоду в журналі
        public bool UseWherePeriod { get; set; } = true;

        protected override void OpenSelect(object? sender, EventArgs args)
        {
            Подія page = new Подія();

            page.DocumentPointerItem = Pointer.UnigueID;
            page.CallBack_OnSelectPointer = (UnigueID selectPointer) =>
            {
                Pointer = new Подія_Pointer(selectPointer);
            };

            Program.GeneralForm?.CreateNotebookPage($"Вибір - {Подія_Const.FULLNAME}", () => { return page; }, true);

            if (UseWherePeriod)
                page.SetValue();
            else
                page.LoadRecords();
        }

        protected override void OnClear(object? sender, EventArgs args)
        {
            Pointer = new Подія_Pointer();
        }
    }
}
    