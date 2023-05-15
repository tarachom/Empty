
/*
Copyright (C) 2019-2023 TARAKHOMYN YURIY IVANOVYCH
All rights reserved.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

/*
Автор:    Тарахомин Юрій Іванович
Адреса:   Україна, м. Львів
Сайт:     accounting.org.ua
*/
  
/*
 *
 * Конфігурації "Нова конфігурація"
 * Автор 
  
 * Дата конфігурації: 15.05.2023 17:51:40
 *
 *
 * Цей код згенерований в Конфігураторі 3. Шаблон Gtk.xslt
 *
 */

using Gtk;
using AccountingSoftware;

namespace StorageAndTrade_1_0.Довідники.ТабличніСписки
{
    
    #region DIRECTORY "Користувачі"
    
      
    /* ТАБЛИЦЯ */
    public class Користувачі_Записи
    {
        string Image 
        {
            get
            {
                return AppContext.BaseDirectory + "images/" + (DeletionLabel ? "doc_delete.png" : "doc.png");
            }
        }

        bool DeletionLabel = false;
        string ID = "";
        
        string Код = "";
        string Назва = "";

        Array ToArray()
        {
            return new object[] { new Gdk.Pixbuf(Image), ID
            /* */ , Код, Назва };
        }

        public static ListStore Store = new ListStore(typeof(Gdk.Pixbuf) /* Image */, typeof(string) /* ID */
            , typeof(string) /* Код */
            , typeof(string) /* Назва */
            );

        public static void AddColumns(TreeView treeView)
        {
            treeView.AppendColumn(new TreeViewColumn("", new CellRendererPixbuf(), "pixbuf", 0)); /* { Ypad = 4 } */
            treeView.AppendColumn(new TreeViewColumn("ID", new CellRendererText(), "text", 1) { Visible = false });
            /* */
            treeView.AppendColumn(new TreeViewColumn("Код", new CellRendererText() { Xpad = 4 }, "text", 2) { MinWidth = 20, Resizable = true, SortColumnId = 2 } ); /*Код*/
            treeView.AppendColumn(new TreeViewColumn("Назва", new CellRendererText() { Xpad = 4 }, "text", 3) { MinWidth = 20, Resizable = true, SortColumnId = 3 } ); /*Назва*/
            
            //Пустишка
            treeView.AppendColumn(new TreeViewColumn());
        }

        public static List<Where> Where { get; set; } = new List<Where>();

        public static UnigueID? DirectoryPointerItem { get; set; }
        public static UnigueID? SelectPointerItem { get; set; }
        public static TreePath? FirstPath;
        public static TreePath? SelectPath;
        public static TreePath? CurrentPath;

        public static void LoadRecords()
        {
            Store.Clear();
            FirstPath = SelectPath = CurrentPath = null;

            Довідники.Користувачі_Select Користувачі_Select = new Довідники.Користувачі_Select();
            Користувачі_Select.QuerySelect.Field.AddRange(
                new string[]
                { "deletion_label" /*Помітка на видалення*/
                    , Довідники.Користувачі_Const.Код /* 1 */
                    , Довідники.Користувачі_Const.Назва /* 2 */
                    
                });

            /* Where */
            Користувачі_Select.QuerySelect.Where = Where;

            
              /* ORDER */
              Користувачі_Select.QuerySelect.Order.Add(Довідники.Користувачі_Const.Назва, SelectOrder.ASC);
            

            /* SELECT */
            Користувачі_Select.Select();
            while (Користувачі_Select.MoveNext())
            {
                Довідники.Користувачі_Pointer? cur = Користувачі_Select.Current;

                if (cur != null)
                {
                    Користувачі_Записи Record = new Користувачі_Записи
                    {
                        ID = cur.UnigueID.ToString(),
                        DeletionLabel = (bool)cur.Fields?["deletion_label"]!, /*Помітка на видалення*/
                        Код = cur.Fields?[Користувачі_Const.Код]?.ToString() ?? "", /**/
                        Назва = cur.Fields?[Користувачі_Const.Назва]?.ToString() ?? "" /**/
                        
                    };

                    TreeIter CurrentIter = Store.AppendValues(Record.ToArray());
                    CurrentPath = Store.GetPath(CurrentIter);

                    if (FirstPath == null)
                        FirstPath = CurrentPath;

                    if (DirectoryPointerItem != null || SelectPointerItem != null)
                    {
                        string UidSelect = SelectPointerItem != null ? SelectPointerItem.ToString() : DirectoryPointerItem!.ToString();

                        if (Record.ID == UidSelect)
                            SelectPath = CurrentPath;
                    }
                }
            }
        }
    }
	    
    /* ТАБЛИЦЯ */
    public class Користувачі_ЗаписиШвидкийВибір
    {
        string Image 
        {
            get
            {
                return AppContext.BaseDirectory + "images/" + (DeletionLabel ? "doc_delete.png" : "doc.png");
            }
        }

        bool DeletionLabel = false;
        string ID = "";
        
        string Код = "";
        string Назва = "";

        Array ToArray()
        {
            return new object[] { new Gdk.Pixbuf(Image), ID
            /* */ , Код, Назва };
        }

        public static ListStore Store = new ListStore(typeof(Gdk.Pixbuf) /* Image */, typeof(string) /* ID */
            , typeof(string) /* Код */
            , typeof(string) /* Назва */
            );

        public static void AddColumns(TreeView treeView)
        {
            treeView.AppendColumn(new TreeViewColumn("", new CellRendererPixbuf(), "pixbuf", 0)); /* { Ypad = 4 } */
            treeView.AppendColumn(new TreeViewColumn("ID", new CellRendererText(), "text", 1) { Visible = false });
            /* */
            treeView.AppendColumn(new TreeViewColumn("Код", new CellRendererText() { Xpad = 4 }, "text", 2) { MinWidth = 20, Resizable = true, SortColumnId = 2 } ); /*Код*/
            treeView.AppendColumn(new TreeViewColumn("Назва", new CellRendererText() { Xpad = 4 }, "text", 3) { MinWidth = 20, Resizable = true, SortColumnId = 3 } ); /*Назва*/
            
            //Пустишка
            treeView.AppendColumn(new TreeViewColumn());
        }

        public static List<Where> Where { get; set; } = new List<Where>();

        public static UnigueID? DirectoryPointerItem { get; set; }
        public static UnigueID? SelectPointerItem { get; set; }
        public static TreePath? FirstPath;
        public static TreePath? SelectPath;
        public static TreePath? CurrentPath;

        public static void LoadRecords()
        {
            Store.Clear();
            FirstPath = SelectPath = CurrentPath = null;

            Довідники.Користувачі_Select Користувачі_Select = new Довідники.Користувачі_Select();
            Користувачі_Select.QuerySelect.Field.AddRange(
                new string[]
                { "deletion_label" /*Помітка на видалення*/
                    , Довідники.Користувачі_Const.Код /* 1 */
                    , Довідники.Користувачі_Const.Назва /* 2 */
                    
                });

            /* Where */
            Користувачі_Select.QuerySelect.Where = Where;

            
              /* ORDER */
              Користувачі_Select.QuerySelect.Order.Add(Довідники.Користувачі_Const.Назва, SelectOrder.ASC);
            

            /* SELECT */
            Користувачі_Select.Select();
            while (Користувачі_Select.MoveNext())
            {
                Довідники.Користувачі_Pointer? cur = Користувачі_Select.Current;

                if (cur != null)
                {
                    Користувачі_ЗаписиШвидкийВибір Record = new Користувачі_ЗаписиШвидкийВибір
                    {
                        ID = cur.UnigueID.ToString(),
                        DeletionLabel = (bool)cur.Fields?["deletion_label"]!, /*Помітка на видалення*/
                        Код = cur.Fields?[Користувачі_Const.Код]?.ToString() ?? "", /**/
                        Назва = cur.Fields?[Користувачі_Const.Назва]?.ToString() ?? "" /**/
                        
                    };

                    TreeIter CurrentIter = Store.AppendValues(Record.ToArray());
                    CurrentPath = Store.GetPath(CurrentIter);

                    if (FirstPath == null)
                        FirstPath = CurrentPath;

                    if (DirectoryPointerItem != null || SelectPointerItem != null)
                    {
                        string UidSelect = SelectPointerItem != null ? SelectPointerItem.ToString() : DirectoryPointerItem!.ToString();

                        if (Record.ID == UidSelect)
                            SelectPath = CurrentPath;
                    }
                }
            }
        }
    }
	    
    #endregion
    
    #region DIRECTORY "Блокнот"
    
      
    /* ТАБЛИЦЯ */
    public class Блокнот_Записи
    {
        string Image 
        {
            get
            {
                return AppContext.BaseDirectory + "images/" + (DeletionLabel ? "doc_delete.png" : "doc.png");
            }
        }

        bool DeletionLabel = false;
        string ID = "";
        
        string Код = "";
        string Назва = "";

        Array ToArray()
        {
            return new object[] { new Gdk.Pixbuf(Image), ID
            /* */ , Код, Назва };
        }

        public static ListStore Store = new ListStore(typeof(Gdk.Pixbuf) /* Image */, typeof(string) /* ID */
            , typeof(string) /* Код */
            , typeof(string) /* Назва */
            );

        public static void AddColumns(TreeView treeView)
        {
            treeView.AppendColumn(new TreeViewColumn("", new CellRendererPixbuf(), "pixbuf", 0)); /* { Ypad = 4 } */
            treeView.AppendColumn(new TreeViewColumn("ID", new CellRendererText(), "text", 1) { Visible = false });
            /* */
            treeView.AppendColumn(new TreeViewColumn("Код", new CellRendererText() { Xpad = 4 }, "text", 2) { MinWidth = 20, Resizable = true, SortColumnId = 2 } ); /*Код*/
            treeView.AppendColumn(new TreeViewColumn("Назва", new CellRendererText() { Xpad = 4 }, "text", 3) { MinWidth = 20, Resizable = true, SortColumnId = 3 } ); /*Назва*/
            
            //Пустишка
            treeView.AppendColumn(new TreeViewColumn());
        }

        public static List<Where> Where { get; set; } = new List<Where>();

        public static UnigueID? DirectoryPointerItem { get; set; }
        public static UnigueID? SelectPointerItem { get; set; }
        public static TreePath? FirstPath;
        public static TreePath? SelectPath;
        public static TreePath? CurrentPath;

        public static void LoadRecords()
        {
            Store.Clear();
            FirstPath = SelectPath = CurrentPath = null;

            Довідники.Блокнот_Select Блокнот_Select = new Довідники.Блокнот_Select();
            Блокнот_Select.QuerySelect.Field.AddRange(
                new string[]
                { "deletion_label" /*Помітка на видалення*/
                    , Довідники.Блокнот_Const.Код /* 1 */
                    , Довідники.Блокнот_Const.Назва /* 2 */
                    
                });

            /* Where */
            Блокнот_Select.QuerySelect.Where = Where;

            
              /* ORDER */
              Блокнот_Select.QuerySelect.Order.Add(Довідники.Блокнот_Const.Код, SelectOrder.ASC);
            

            /* SELECT */
            Блокнот_Select.Select();
            while (Блокнот_Select.MoveNext())
            {
                Довідники.Блокнот_Pointer? cur = Блокнот_Select.Current;

                if (cur != null)
                {
                    Блокнот_Записи Record = new Блокнот_Записи
                    {
                        ID = cur.UnigueID.ToString(),
                        DeletionLabel = (bool)cur.Fields?["deletion_label"]!, /*Помітка на видалення*/
                        Код = cur.Fields?[Блокнот_Const.Код]?.ToString() ?? "", /**/
                        Назва = cur.Fields?[Блокнот_Const.Назва]?.ToString() ?? "" /**/
                        
                    };

                    TreeIter CurrentIter = Store.AppendValues(Record.ToArray());
                    CurrentPath = Store.GetPath(CurrentIter);

                    if (FirstPath == null)
                        FirstPath = CurrentPath;

                    if (DirectoryPointerItem != null || SelectPointerItem != null)
                    {
                        string UidSelect = SelectPointerItem != null ? SelectPointerItem.ToString() : DirectoryPointerItem!.ToString();

                        if (Record.ID == UidSelect)
                            SelectPath = CurrentPath;
                    }
                }
            }
        }
    }
	    
    /* ТАБЛИЦЯ */
    public class Блокнот_ЗаписиШвидкийВибір
    {
        string Image 
        {
            get
            {
                return AppContext.BaseDirectory + "images/" + (DeletionLabel ? "doc_delete.png" : "doc.png");
            }
        }

        bool DeletionLabel = false;
        string ID = "";
        
        string Код = "";
        string Назва = "";

        Array ToArray()
        {
            return new object[] { new Gdk.Pixbuf(Image), ID
            /* */ , Код, Назва };
        }

        public static ListStore Store = new ListStore(typeof(Gdk.Pixbuf) /* Image */, typeof(string) /* ID */
            , typeof(string) /* Код */
            , typeof(string) /* Назва */
            );

        public static void AddColumns(TreeView treeView)
        {
            treeView.AppendColumn(new TreeViewColumn("", new CellRendererPixbuf(), "pixbuf", 0)); /* { Ypad = 4 } */
            treeView.AppendColumn(new TreeViewColumn("ID", new CellRendererText(), "text", 1) { Visible = false });
            /* */
            treeView.AppendColumn(new TreeViewColumn("Код", new CellRendererText() { Xpad = 4 }, "text", 2) { MinWidth = 20, Resizable = true, SortColumnId = 2 } ); /*Код*/
            treeView.AppendColumn(new TreeViewColumn("Назва", new CellRendererText() { Xpad = 4 }, "text", 3) { MinWidth = 20, Resizable = true, SortColumnId = 3 } ); /*Назва*/
            
            //Пустишка
            treeView.AppendColumn(new TreeViewColumn());
        }

        public static List<Where> Where { get; set; } = new List<Where>();

        public static UnigueID? DirectoryPointerItem { get; set; }
        public static UnigueID? SelectPointerItem { get; set; }
        public static TreePath? FirstPath;
        public static TreePath? SelectPath;
        public static TreePath? CurrentPath;

        public static void LoadRecords()
        {
            Store.Clear();
            FirstPath = SelectPath = CurrentPath = null;

            Довідники.Блокнот_Select Блокнот_Select = new Довідники.Блокнот_Select();
            Блокнот_Select.QuerySelect.Field.AddRange(
                new string[]
                { "deletion_label" /*Помітка на видалення*/
                    , Довідники.Блокнот_Const.Код /* 1 */
                    , Довідники.Блокнот_Const.Назва /* 2 */
                    
                });

            /* Where */
            Блокнот_Select.QuerySelect.Where = Where;

            

            /* SELECT */
            Блокнот_Select.Select();
            while (Блокнот_Select.MoveNext())
            {
                Довідники.Блокнот_Pointer? cur = Блокнот_Select.Current;

                if (cur != null)
                {
                    Блокнот_ЗаписиШвидкийВибір Record = new Блокнот_ЗаписиШвидкийВибір
                    {
                        ID = cur.UnigueID.ToString(),
                        DeletionLabel = (bool)cur.Fields?["deletion_label"]!, /*Помітка на видалення*/
                        Код = cur.Fields?[Блокнот_Const.Код]?.ToString() ?? "", /**/
                        Назва = cur.Fields?[Блокнот_Const.Назва]?.ToString() ?? "" /**/
                        
                    };

                    TreeIter CurrentIter = Store.AppendValues(Record.ToArray());
                    CurrentPath = Store.GetPath(CurrentIter);

                    if (FirstPath == null)
                        FirstPath = CurrentPath;

                    if (DirectoryPointerItem != null || SelectPointerItem != null)
                    {
                        string UidSelect = SelectPointerItem != null ? SelectPointerItem.ToString() : DirectoryPointerItem!.ToString();

                        if (Record.ID == UidSelect)
                            SelectPath = CurrentPath;
                    }
                }
            }
        }
    }
	    
    #endregion
    
}

namespace StorageAndTrade_1_0.Документи.ТабличніСписки
{
    public static class Інтерфейс
    {
        public static ComboBoxText СписокВідбірПоПеріоду()
        {
            ComboBoxText сomboBox = new ComboBoxText();

            if (Config.Kernel != null)
            {
                ConfigurationEnums ТипПеріодуДляЖурналівДокументів = Config.Kernel.Conf.Enums["ТипПеріодуДляЖурналівДокументів"];

                foreach (ConfigurationEnumField field in ТипПеріодуДляЖурналівДокументів.Fields.Values)
                    сomboBox.Append(field.Name, field.Desc);
            }

            /*сomboBox.Active = 0;*/

            return сomboBox;
        }

        public static void ДодатиВідбірПоПеріоду(List<Where> Where, string fieldWhere, Перелічення.ТипПеріодуДляЖурналівДокументів типПеріоду)
        {
            switch (типПеріоду)
            {
                case Перелічення.ТипПеріодуДляЖурналівДокументів.ЗПочаткуРоку:
                {
                    Where.Add(new Where(fieldWhere, Comparison.QT_EQ, new DateTime(DateTime.Now.Year, 1, 1)));
                    break;
                }
                case Перелічення.ТипПеріодуДляЖурналівДокументів.Квартал:
                {
                    DateTime ДатаТриМісцяНазад = DateTime.Now.AddMonths(-3);
                    Where.Add(new Where(fieldWhere, Comparison.QT_EQ, new DateTime(ДатаТриМісцяНазад.Year, ДатаТриМісцяНазад.Month, 1)));
                    break;
                }
                case Перелічення.ТипПеріодуДляЖурналівДокументів.ЗМинулогоМісяця:
                {
                    DateTime ДатаМісцьНазад = DateTime.Now.AddMonths(-1);
                    Where.Add(new Where(fieldWhere, Comparison.QT_EQ, new DateTime(ДатаМісцьНазад.Year, ДатаМісцьНазад.Month, 1)));
                    break;
                }
                case Перелічення.ТипПеріодуДляЖурналівДокументів.Місяць:
                {
                    Where.Add(new Where(fieldWhere, Comparison.QT_EQ, DateTime.Now.AddMonths(-1)));
                    break;
                }
                case Перелічення.ТипПеріодуДляЖурналівДокументів.ЗПочаткуМісяця:
                {
                    Where.Add(new Where(fieldWhere, Comparison.QT_EQ, new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)));
                    break;
                }
                case Перелічення.ТипПеріодуДляЖурналівДокументів.ЗПочаткуТижня:
                {
                    DateTime СімДнівНазад = DateTime.Now.AddDays(-7);
                    Where.Add(new Where(fieldWhere, Comparison.QT_EQ, new DateTime(СімДнівНазад.Year, СімДнівНазад.Month, СімДнівНазад.Day)));
                    break;
                }
                case Перелічення.ТипПеріодуДляЖурналівДокументів.ПоточнийДень:
                {
                    Where.Add(new Where(fieldWhere, Comparison.QT_EQ, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)));
                    break;
                }
            }
        }
    }

    
    #region DOCUMENT "Подія"
    
      
    public class Подія_Записи
    {
        string Image 
        {
            get
            {
                return AppContext.BaseDirectory + "images/" + (DeletionLabel ? "doc_delete.png" : "doc.png");
            }
        }

        bool DeletionLabel = false;
        bool Spend = false;
        string ID = "";
        
        string Назва = "";
        string ДатаДок = "";
        string НомерДок = "";
        string Коментар = "";
        string Користувач = "";
        string Блокнот = "";
        string Період = "";
        string Ціна = "";
        string Сума = "";
        string Заблоковано = "";
        string ДатаДоставки = "";
        string ЧасДоставки = "";
        string ЗадаєтьсяКористувачем = "";

        Array ToArray()
        {
            return new object[] { new Gdk.Pixbuf(Image), ID, Spend /*Проведений документ*/
            /* */ , Назва, ДатаДок, НомерДок, Коментар, Користувач, Блокнот, Період, Ціна, Сума, Заблоковано, ДатаДоставки, ЧасДоставки, ЗадаєтьсяКористувачем };
        }

        public static ListStore Store = new ListStore(typeof(Gdk.Pixbuf) /* Image */, typeof(string) /* ID */, typeof(bool) /* Spend Проведений документ*/
            , typeof(string) /* Назва */
            , typeof(string) /* ДатаДок */
            , typeof(string) /* НомерДок */
            , typeof(string) /* Коментар */
            , typeof(string) /* Користувач */
            , typeof(string) /* Блокнот */
            , typeof(string) /* Період */
            , typeof(string) /* Ціна */
            , typeof(string) /* Сума */
            , typeof(string) /* Заблоковано */
            , typeof(string) /* ДатаДоставки */
            , typeof(string) /* ЧасДоставки */
            , typeof(string) /* ЗадаєтьсяКористувачем */
            );

        public static void AddColumns(TreeView treeView)
        {
            treeView.AppendColumn(new TreeViewColumn("", new CellRendererPixbuf(), "pixbuf", 0)); /*Image*/ /* { Ypad = 0 } */
            treeView.AppendColumn(new TreeViewColumn("ID", new CellRendererText(), "text", 1) { Visible = false }); /*UID*/
            treeView.AppendColumn(new TreeViewColumn("", new CellRendererToggle(), "active", 2)); /*Проведений документ*/
            /* */
            treeView.AppendColumn(new TreeViewColumn("Назва", new CellRendererText() { Xpad = 4 }, "text", 3) { MinWidth = 20, Resizable = true } ); /*Назва*/
            treeView.AppendColumn(new TreeViewColumn("ДатаДок", new CellRendererText() { Xpad = 4 }, "text", 4) { MinWidth = 20, Resizable = true } ); /*ДатаДок*/
            treeView.AppendColumn(new TreeViewColumn("НомерДок", new CellRendererText() { Xpad = 4 }, "text", 5) { MinWidth = 20, Resizable = true } ); /*НомерДок*/
            treeView.AppendColumn(new TreeViewColumn("Коментар", new CellRendererText() { Xpad = 4 }, "text", 6) { MinWidth = 20, Resizable = true } ); /*Коментар*/
            treeView.AppendColumn(new TreeViewColumn("Користувач", new CellRendererText() { Xpad = 4 }, "text", 7) { MinWidth = 20, Resizable = true } ); /*Користувач*/
            treeView.AppendColumn(new TreeViewColumn("Блокнот", new CellRendererText() { Xpad = 4 }, "text", 8) { MinWidth = 20, Resizable = true } ); /*Блокнот*/
            treeView.AppendColumn(new TreeViewColumn("Період", new CellRendererText() { Xpad = 4 }, "text", 9) { MinWidth = 20, Resizable = true } ); /*Період*/
            treeView.AppendColumn(new TreeViewColumn("Ціна", new CellRendererText() { Xpad = 4 }, "text", 10) { MinWidth = 20, Resizable = true } ); /*Ціна*/
            treeView.AppendColumn(new TreeViewColumn("Сума", new CellRendererText() { Xpad = 4 }, "text", 11) { MinWidth = 20, Resizable = true } ); /*Сума*/
            treeView.AppendColumn(new TreeViewColumn("Заблоковано", new CellRendererText() { Xpad = 4 }, "text", 12) { MinWidth = 20, Resizable = true } ); /*Заблоковано*/
            treeView.AppendColumn(new TreeViewColumn("ДатаДоставки", new CellRendererText() { Xpad = 4 }, "text", 13) { MinWidth = 20, Resizable = true } ); /*ДатаДоставки*/
            treeView.AppendColumn(new TreeViewColumn("ЧасДоставки", new CellRendererText() { Xpad = 4 }, "text", 14) { MinWidth = 20, Resizable = true } ); /*ЧасДоставки*/
            treeView.AppendColumn(new TreeViewColumn("ЗадаєтьсяКористувачем", new CellRendererText() { Xpad = 4 }, "text", 15) { MinWidth = 20, Resizable = true } ); /*ЗадаєтьсяКористувачем*/
            
            //Пустишка
            treeView.AppendColumn(new TreeViewColumn());
        }

        public static List<Where> Where { get; set; } = new List<Where>();

        public static void ДодатиВідбірПоПеріоду(Перелічення.ТипПеріодуДляЖурналівДокументів типПеріоду)
        {
            Where.Clear();
            Інтерфейс.ДодатиВідбірПоПеріоду(Where, Документи.Подія_Const.ДатаДок, типПеріоду);
        }

        public static UnigueID? DocumentPointerItem { get; set; }
        public static UnigueID? SelectPointerItem { get; set; }
        public static TreePath? FirstPath;
        public static TreePath? SelectPath;
        public static TreePath? CurrentPath;

        public static void LoadRecords()
        {
            Store.Clear();
            FirstPath = SelectPath = CurrentPath = null;

            Документи.Подія_Select Подія_Select = new Документи.Подія_Select();
            Подія_Select.QuerySelect.Field.AddRange(
                new string[]
                { "deletion_label" /*Помітка на видалення*/,
                  "spend" /*Проведений документ*/
                    , Документи.Подія_Const.Назва /* 1 */
                    , Документи.Подія_Const.ДатаДок /* 2 */
                    , Документи.Подія_Const.НомерДок /* 3 */
                    , Документи.Подія_Const.Коментар /* 4 */
                    , Документи.Подія_Const.Період /* 5 */
                    , Документи.Подія_Const.Ціна /* 6 */
                    , Документи.Подія_Const.Сума /* 7 */
                    , Документи.Подія_Const.Заблоковано /* 8 */
                    , Документи.Подія_Const.ДатаДоставки /* 9 */
                    , Документи.Подія_Const.ЧасДоставки /* 10 */
                    , Документи.Подія_Const.ЗадаєтьсяКористувачем /* 11 */
                    
                });

            /* Where */
            Подія_Select.QuerySelect.Where = Where;

            
              /* ORDER */
              Подія_Select.QuerySelect.Order.Add(Документи.Подія_Const.ДатаДок, SelectOrder.ASC);
            
                /* Join Table */
                Подія_Select.QuerySelect.Joins.Add(
                    new Join(Довідники.Користувачі_Const.TABLE, Документи.Подія_Const.Користувач, Подія_Select.QuerySelect.Table, "join_tab_1"));
                
                  /* Field */
                  Подія_Select.QuerySelect.FieldAndAlias.Add(
                    new NameValue<string>("join_tab_1." + Довідники.Користувачі_Const.Назва, "join_tab_1_field_1"));
                  
                /* Join Table */
                Подія_Select.QuerySelect.Joins.Add(
                    new Join(Довідники.Блокнот_Const.TABLE, Документи.Подія_Const.Блокнот, Подія_Select.QuerySelect.Table, "join_tab_2"));
                
                  /* Field */
                  Подія_Select.QuerySelect.FieldAndAlias.Add(
                    new NameValue<string>("join_tab_2." + Довідники.Блокнот_Const.Назва, "join_tab_2_field_1"));
                  

            /* SELECT */
            Подія_Select.Select();
            while (Подія_Select.MoveNext())
            {
                Документи.Подія_Pointer? cur = Подія_Select.Current;

                if (cur != null)
                {
                    Подія_Записи Record = new Подія_Записи
                    {
                        ID = cur.UnigueID.ToString(),
                        Spend = (bool)cur.Fields?["spend"]!, /*Проведений документ*/
                        DeletionLabel = (bool)cur.Fields?["deletion_label"]!, /*Помітка на видалення*/
                        Користувач = cur.Fields?["join_tab_1_field_1"]?.ToString() ?? "", /**/
                        Блокнот = cur.Fields?["join_tab_2_field_1"]?.ToString() ?? "", /**/
                        Назва = cur.Fields?[Подія_Const.Назва]?.ToString() ?? "", /**/
                        ДатаДок = cur.Fields?[Подія_Const.ДатаДок]?.ToString() ?? "", /**/
                        НомерДок = cur.Fields?[Подія_Const.НомерДок]?.ToString() ?? "", /**/
                        Коментар = cur.Fields?[Подія_Const.Коментар]?.ToString() ?? "", /**/
                        Період = Перелічення.ПсевдонімиПерелічення.ТипПеріодуДляЖурналівДокументів_Alias( ((Перелічення.ТипПеріодуДляЖурналівДокументів)(cur.Fields?[Подія_Const.Період]! != DBNull.Value ? cur.Fields?[Подія_Const.Період]! : 0)) ), /**/
                        Ціна = cur.Fields?[Подія_Const.Ціна]?.ToString() ?? "", /**/
                        Сума = cur.Fields?[Подія_Const.Сума]?.ToString() ?? "", /**/
                        Заблоковано = (cur.Fields?[Подія_Const.Заблоковано]! != DBNull.Value ? (bool)cur.Fields?[Подія_Const.Заблоковано]! : false) ? "Так" : "", /**/
                        ДатаДоставки = cur.Fields?[Подія_Const.ДатаДоставки]?.ToString() ?? "", /**/
                        ЧасДоставки = cur.Fields?[Подія_Const.ЧасДоставки]?.ToString() ?? "", /**/
                        ЗадаєтьсяКористувачем = cur.Fields?[Подія_Const.ЗадаєтьсяКористувачем]?.ToString() ?? "" /**/
                        
                    };

                    TreeIter CurrentIter = Store.AppendValues(Record.ToArray());
                    CurrentPath = Store.GetPath(CurrentIter);

                    if (FirstPath == null)
                        FirstPath = CurrentPath;

                    if (DocumentPointerItem != null || SelectPointerItem != null)
                    {
                        string UidSelect = SelectPointerItem != null ? SelectPointerItem.ToString() : DocumentPointerItem!.ToString();

                        if (Record.ID == UidSelect)
                            SelectPath = CurrentPath;
                    }
                }
            }
        }
    }
	    
    #endregion
    

    //
    // Журнали
    //

    
    #region JOURNAL "Повний"
    
    public class Журнали_Повний
    {
        string Image 
        {
            get
            {
                return AppContext.BaseDirectory + "images/" + (DeletionLabel ? "doc_delete.png" : "doc.png");
            }
        }

        bool DeletionLabel = false;
        bool Spend = false;
        string ID = "";
        string Type = ""; //Тип документу
        
        string Назва = "";
        string Дата = "";
        string Номер = "";
        string Коментар = "";

        // Масив для запису стрічки в Store
        Array ToArray()
        {
            return new object[] { new Gdk.Pixbuf(Image), ID, Type, Spend /*Проведений документ*/
            /* */ , Назва, Дата, Номер, Коментар };
        }

        // Джерело даних для списку
        public static ListStore Store = new ListStore(
          typeof(Gdk.Pixbuf) /* Image */, 
          typeof(string) /* ID */, 
          typeof(string) /* Type */, 
          typeof(bool) /* Spend Проведений документ*/
            , typeof(string) /* Назва */
            , typeof(string) /* Дата */
            , typeof(string) /* Номер */
            , typeof(string) /* Коментар */
            );

        // Добавлення колонок в список
        public static void AddColumns(TreeView treeView)
        {
            treeView.AppendColumn(new TreeViewColumn("", new CellRendererPixbuf(), "pixbuf", 0)); /*Image*/ /* { Ypad = 0 } */
            treeView.AppendColumn(new TreeViewColumn("ID", new CellRendererText(), "text", 1) { Visible = false }); /*UID*/
            treeView.AppendColumn(new TreeViewColumn("Type", new CellRendererText(), "text", 2) { Visible = false }); /*Type*/
            treeView.AppendColumn(new TreeViewColumn("", new CellRendererToggle(), "active", 3)); /*Проведений документ*/
            /* */
            treeView.AppendColumn(new TreeViewColumn("Назва", new CellRendererText() { Xpad = 4 }, "text", 4) { MinWidth = 20, Resizable = true } ); /*Назва*/
            treeView.AppendColumn(new TreeViewColumn("Дата", new CellRendererText() { Xpad = 4 }, "text", 5) { MinWidth = 20, Resizable = true } ); /*Дата*/
            treeView.AppendColumn(new TreeViewColumn("Номер", new CellRendererText() { Xpad = 4 }, "text", 6) { MinWidth = 20, Resizable = true } ); /*Номер*/
            treeView.AppendColumn(new TreeViewColumn("Коментар", new CellRendererText() { Xpad = 4 }, "text", 7) { MinWidth = 20, Resizable = true } ); /*Коментар*/
            
            //Пустишка
            treeView.AppendColumn(new TreeViewColumn());
        }

        // Словник з відборами, ключ це Тип документу
        public static Dictionary<string, List<Where>> Where { get; set; } = new Dictionary<string, List<Where>>();

        // Добавляє відбір по періоду в словник відборів
        public static void ДодатиВідбірПоПеріоду(Перелічення.ТипПеріодуДляЖурналівДокументів типПеріоду)
        {
            Where.Clear();
            
            {
                List<Where> where = new List<Where>();
                Where.Add("Подія", where);
                Інтерфейс.ДодатиВідбірПоПеріоду(where, Подія_Const.ДатаДок, типПеріоду);
            }
              
        }

        // Список документів які входять в журнал
        public static Dictionary<string, string> AllowDocument()
        {
            Dictionary<string, string> allowDoc = new Dictionary<string, string>();
            allowDoc.Add("Подія", "Подія");
            
            return allowDoc;
        }

        public static UnigueID? SelectPointerItem { get; set; }
        public static TreePath? SelectPath;
        public static TreePath? CurrentPath;

        // Завантаження даних
        public static void LoadRecords()
        {
            Store.Clear();
            SelectPath = CurrentPath = null;
            List<string> allQuery = new List<string>();
            Dictionary<string, object> paramQuery = new Dictionary<string, object>();

          
              {
                  Query query = new Query(Документи.Подія_Const.TABLE);

                  // Встановлення відбору для даного типу документу
                  if (Where.ContainsKey("Подія") && Where["Подія"].Count != 0) {
                      query.Where = Where["Подія"];
                      foreach(Where field in query.Where)
                          paramQuery.Add(field.Alias, field.Value);
                  }

                  query.FieldAndAlias.Add(new NameValue<string>("'Подія'", "type"));
                  query.Field.Add("deletion_label");
                  query.Field.Add("spend");
                  
                              query.FieldAndAlias.Add(
                                  new NameValue<string>(Документи.Подія_Const.TABLE + "." + Документи.Подія_Const.Назва, "Назва"));
                            
                              query.FieldAndAlias.Add(
                                  new NameValue<string>(Документи.Подія_Const.TABLE + "." + Документи.Подія_Const.ДатаДок, "Дата"));
                            
                              query.FieldAndAlias.Add(
                                  new NameValue<string>(Документи.Подія_Const.TABLE + "." + Документи.Подія_Const.НомерДок, "Номер"));
                            
                              query.FieldAndAlias.Add(
                                  new NameValue<string>(Документи.Подія_Const.TABLE + "." + Документи.Подія_Const.Коментар, "Коментар"));
                            

                  allQuery.Add(query.Construct());
              }
              

            string unionAllQuery = string.Join("\nUNION\n", allQuery);

            unionAllQuery += "\nORDER BY Дата";
          
            string[] columnsName;
            List<Dictionary<string, object>> listRow;

            Config.Kernel!.DataBase.SelectRequest(unionAllQuery, paramQuery, out columnsName, out listRow);

            foreach (Dictionary<string, object> row in listRow)
            {
                Журнали_Повний record = new Журнали_Повний();
                record.ID = row["uid"]?.ToString() ?? "";
                record.Type = row["type"]?.ToString() ?? "";
                record.DeletionLabel = (bool)row["deletion_label"];
                record.Spend = (bool)row["spend"];
                
                    record.Назва = row["Назва"] != DBNull.Value ? (row["Назва"]?.ToString() ?? "") : "";
                
                    record.Дата = row["Дата"] != DBNull.Value ? (row["Дата"]?.ToString() ?? "") : "";
                
                    record.Номер = row["Номер"] != DBNull.Value ? (row["Номер"]?.ToString() ?? "") : "";
                
                    record.Коментар = row["Коментар"] != DBNull.Value ? (row["Коментар"]?.ToString() ?? "") : "";
                

                TreeIter CurrentIter = Store.AppendValues(record.ToArray());
                CurrentPath = Store.GetPath(CurrentIter);

                if (SelectPointerItem != null)
                {
                    if (record.ID == SelectPointerItem.ToString())
                        SelectPath = CurrentPath;
                }
            }
          
        }
    }
    #endregion
    
}

namespace StorageAndTrade_1_0.РегістриВідомостей.ТабличніСписки
{
    
}

  