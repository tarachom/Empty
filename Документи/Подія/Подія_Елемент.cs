

/*
        Подія_Елемент.cs
        Елемент
*/

using Gtk;

using AccountingSoftware;

using StorageAndTrade_1_0;
using StorageAndTrade_1_0.Константи;
using StorageAndTrade_1_0.Довідники;
using StorageAndTrade_1_0.Документи;
using StorageAndTrade_1_0.Перелічення;

namespace StorageAndTrade
{
    class Подія_Елемент : ДокументЕлемент
    {
        public Подія_Objest Подія_Objest { get; set; } = new Подія_Objest();

        #region Fields

        Entry Назва = new Entry() { WidthRequest = 500 };

        DateTimeControl ДатаДок = new DateTimeControl();

        Entry НомерДок = new Entry() { WidthRequest = 500 };

        Entry Коментар = new Entry() { WidthRequest = 500 };
        Користувачі_PointerControl Користувач = new Користувачі_PointerControl() { Caption = "Користувач", WidthPresentation = 300 };
        Блокнот_PointerControl Блокнот = new Блокнот_PointerControl() { Caption = "Блокнот", WidthPresentation = 300 };

        ComboBoxText Період = new ComboBoxText();

        NumericControl Ціна = new NumericControl();

        NumericControl Сума = new NumericControl();

        CheckButton Заблоковано = new CheckButton("Заблоковано");

        DateTimeControl ДатаДоставки = new DateTimeControl();

        TimeControl ЧасДоставки = new TimeControl();

        CompositePointerControl ЗадаєтьсяКористувачем = new CompositePointerControl();

        #endregion

        #region TabularParts

        #endregion

        public Подія_Елемент() : base()
        {
            CreateDocName(Подія_Const.FULLNAME, НомерДок, ДатаДок);


            CreateField(HBoxComment, "Коментар:", Коментар);


            FillComboBoxes();
        }

        void FillComboBoxes()
        {

            foreach (var field in ПсевдонімиПерелічення.ТипПеріодуДляЖурналівДокументів_List())
                Період.Append(field.Value.ToString(), field.Name);

        }

        protected override void CreateContainer1(VBox vBox)
        {

        }

        protected override void CreateContainer2(VBox vBox)
        {

        }

        protected override void CreateContainer3(VBox vBox)
        {

            //Назва
            CreateField(vBox, "Назва:", Назва);

            //Користувач
            CreateField(vBox, null, Користувач);

            //Блокнот
            CreateField(vBox, null, Блокнот);

            //Період
            CreateField(vBox, "Період:", Період);

            //Ціна
            CreateField(vBox, "Ціна:", Ціна);

            //Сума
            CreateField(vBox, "Сума:", Сума);

            //Заблоковано
            CreateField(vBox, null, Заблоковано);

            //ДатаДоставки
            CreateField(vBox, "ДатаДоставки:", ДатаДоставки);

            //ЧасДоставки
            CreateField(vBox, "ЧасДоставки:", ЧасДоставки);

            //ЗадаєтьсяКористувачем
            CreateField(vBox, null, ЗадаєтьсяКористувачем);

        }

        protected override void CreateContainer4(VBox vBox)
        {

        }

        #region Присвоєння / зчитування значень

        public override void SetValue()
        {
            if (IsNew)
                Подія_Objest.New();

            Назва.Text = Подія_Objest.Назва;
            ДатаДок.Value = Подія_Objest.ДатаДок;
            НомерДок.Text = Подія_Objest.НомерДок;
            Коментар.Text = Подія_Objest.Коментар;
            Користувач.Pointer = Подія_Objest.Користувач;
            Блокнот.Pointer = Подія_Objest.Блокнот;
            Період.ActiveId = Подія_Objest.Період.ToString();
            if (Період.Active == -1) Період.Active = 0;
            Ціна.Value = Подія_Objest.Ціна;
            Сума.Value = Подія_Objest.Сума;
            Заблоковано.Active = Подія_Objest.Заблоковано;
            ДатаДоставки.Value = Подія_Objest.ДатаДоставки;
            ЧасДоставки.Value = Подія_Objest.ЧасДоставки;
            ЗадаєтьсяКористувачем.Pointer = Подія_Objest.ЗадаєтьсяКористувачем;

        }

        protected override void GetValue()
        {
            Подія_Objest.Назва = Назва.Text;
            Подія_Objest.ДатаДок = ДатаДок.Value;
            Подія_Objest.НомерДок = НомерДок.Text;
            Подія_Objest.Коментар = Коментар.Text;
            Подія_Objest.Користувач = Користувач.Pointer;
            Подія_Objest.Блокнот = Блокнот.Pointer;
            if (Період.Active != -1)
                Подія_Objest.Період = Enum.Parse<ТипПеріодуДляЖурналівДокументів>(Період.ActiveId);
            Подія_Objest.Ціна = Ціна.Value;
            Подія_Objest.Сума = Сума.Value;
            Подія_Objest.Заблоковано = Заблоковано.Active;
            Подія_Objest.ДатаДоставки = ДатаДоставки.Value;
            Подія_Objest.ЧасДоставки = ЧасДоставки.Value;
            Подія_Objest.ЗадаєтьсяКористувачем = ЗадаєтьсяКористувачем.Pointer;

        }

        #endregion

        protected override bool Save()
        {
            bool isSave = false;

            try
            {
                isSave = Подія_Objest.Save();
            }
            catch (Exception ex)
            {
                MsgError(ex);
                return false;
            }



            UnigueID = Подія_Objest.UnigueID;
            Caption = Подія_Objest.Назва;

            return isSave;
        }

        protected override bool SpendTheDocument(bool spendDoc)
        {
            if (spendDoc)
            {
                bool isSpend = Подія_Objest.SpendTheDocument(Подія_Objest.ДатаДок);

                if (!isSpend)
                    ФункціїДляПовідомлень.ВідкритиТермінал();

                return isSpend;
            }
            else
            {
                Подія_Objest.ClearSpendTheDocument();

                return true;
            }
        }
    }
}
