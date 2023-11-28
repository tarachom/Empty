
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
  
 * Дата конфігурації: 28.11.2023 09:30:07
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
        string Image { get { return AppContext.BaseDirectory + "images/" + (DeletionLabel ? "doc_delete.png" : "doc.png"); } }
        bool DeletionLabel = false;
        string ID = "";
        
        string Код = "";
        string Назва = "";

        Array ToArray()
        {
            return new object[] { new Gdk.Pixbuf(Image), ID,
            Код, Назва,  };
        }

        public static ListStore Store = new ListStore([/*Image*/ typeof(Gdk.Pixbuf), /*ID*/ typeof(string),
            /*Код*/ typeof(string), /*Назва*/ typeof(string), ]);

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

        public static List<Where> Where { get; set; } = [];

        public static UnigueID? DirectoryPointerItem { get; set; }
        public static UnigueID? SelectPointerItem { get; set; }
        public static TreePath? FirstPath;
        public static TreePath? SelectPath;
        public static TreePath? CurrentPath;

        public static async ValueTask LoadRecords()
        {
            Store.Clear();
            FirstPath = SelectPath = CurrentPath = null;

            Довідники.Користувачі_Select Користувачі_Select = new Довідники.Користувачі_Select();
            Користувачі_Select.QuerySelect.Field.AddRange(
                ["deletion_label",
                  /*Код*/ Довідники.Користувачі_Const.Код,
                  /*Назва*/ Довідники.Користувачі_Const.Назва,
                  
                ]);

            /* Where */
            Користувачі_Select.QuerySelect.Where = Where;

            
              /* ORDER */
              Користувачі_Select.QuerySelect.Order.Add(Довідники.Користувачі_Const.Назва, SelectOrder.ASC);
            

            /* SELECT */
            await Користувачі_Select.Select();
            while (Користувачі_Select.MoveNext())
            {
                Довідники.Користувачі_Pointer? cur = Користувачі_Select.Current;

                if (cur != null)
                {
                    Користувачі_Записи Record = new Користувачі_Записи
                    {
                        ID = cur.UnigueID.ToString(),
                        DeletionLabel = (bool)cur.Fields?["deletion_label"]!, /*Помітка на видалення*/
                        Код = cur.Fields[Користувачі_Const.Код].ToString() ?? "", /**/
                        Назва = cur.Fields[Користувачі_Const.Назва].ToString() ?? "" /**/
                        
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
        string Image { get { return AppContext.BaseDirectory + "images/" + (DeletionLabel ? "doc_delete.png" : "doc.png"); } }
        bool DeletionLabel = false;
        string ID = "";
        
        string Код = "";
        string Назва = "";

        Array ToArray()
        {
            return new object[] { new Gdk.Pixbuf(Image), ID,
            Код, Назва,  };
        }

        public static ListStore Store = new ListStore([/*Image*/ typeof(Gdk.Pixbuf), /*ID*/ typeof(string),
            /*Код*/ typeof(string), /*Назва*/ typeof(string), ]);

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

        public static List<Where> Where { get; set; } = [];

        public static UnigueID? DirectoryPointerItem { get; set; }
        public static UnigueID? SelectPointerItem { get; set; }
        public static TreePath? FirstPath;
        public static TreePath? SelectPath;
        public static TreePath? CurrentPath;

        public static async ValueTask LoadRecords()
        {
            Store.Clear();
            FirstPath = SelectPath = CurrentPath = null;

            Довідники.Користувачі_Select Користувачі_Select = new Довідники.Користувачі_Select();
            Користувачі_Select.QuerySelect.Field.AddRange(
                ["deletion_label",
                  /*Код*/ Довідники.Користувачі_Const.Код,
                  /*Назва*/ Довідники.Користувачі_Const.Назва,
                  
                ]);

            /* Where */
            Користувачі_Select.QuerySelect.Where = Where;

            
              /* ORDER */
              Користувачі_Select.QuerySelect.Order.Add(Довідники.Користувачі_Const.Назва, SelectOrder.ASC);
            

            /* SELECT */
            await Користувачі_Select.Select();
            while (Користувачі_Select.MoveNext())
            {
                Довідники.Користувачі_Pointer? cur = Користувачі_Select.Current;

                if (cur != null)
                {
                    Користувачі_ЗаписиШвидкийВибір Record = new Користувачі_ЗаписиШвидкийВибір
                    {
                        ID = cur.UnigueID.ToString(),
                        DeletionLabel = (bool)cur.Fields?["deletion_label"]!, /*Помітка на видалення*/
                        Код = cur.Fields[Користувачі_Const.Код].ToString() ?? "", /**/
                        Назва = cur.Fields[Користувачі_Const.Назва].ToString() ?? "" /**/
                        
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
        string Image { get { return AppContext.BaseDirectory + "images/" + (DeletionLabel ? "doc_delete.png" : "doc.png"); } }
        bool DeletionLabel = false;
        string ID = "";
        
        string Код = "";
        string Назва = "";

        Array ToArray()
        {
            return new object[] { new Gdk.Pixbuf(Image), ID,
            Код, Назва,  };
        }

        public static ListStore Store = new ListStore([/*Image*/ typeof(Gdk.Pixbuf), /*ID*/ typeof(string),
            /*Код*/ typeof(string), /*Назва*/ typeof(string), ]);

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

        public static List<Where> Where { get; set; } = [];

        public static UnigueID? DirectoryPointerItem { get; set; }
        public static UnigueID? SelectPointerItem { get; set; }
        public static TreePath? FirstPath;
        public static TreePath? SelectPath;
        public static TreePath? CurrentPath;

        public static async ValueTask LoadRecords()
        {
            Store.Clear();
            FirstPath = SelectPath = CurrentPath = null;

            Довідники.Блокнот_Select Блокнот_Select = new Довідники.Блокнот_Select();
            Блокнот_Select.QuerySelect.Field.AddRange(
                ["deletion_label",
                  /*Код*/ Довідники.Блокнот_Const.Код,
                  /*Назва*/ Довідники.Блокнот_Const.Назва,
                  
                ]);

            /* Where */
            Блокнот_Select.QuerySelect.Where = Where;

            
              /* ORDER */
              Блокнот_Select.QuerySelect.Order.Add(Довідники.Блокнот_Const.Код, SelectOrder.ASC);
            

            /* SELECT */
            await Блокнот_Select.Select();
            while (Блокнот_Select.MoveNext())
            {
                Довідники.Блокнот_Pointer? cur = Блокнот_Select.Current;

                if (cur != null)
                {
                    Блокнот_Записи Record = new Блокнот_Записи
                    {
                        ID = cur.UnigueID.ToString(),
                        DeletionLabel = (bool)cur.Fields?["deletion_label"]!, /*Помітка на видалення*/
                        Код = cur.Fields[Блокнот_Const.Код].ToString() ?? "", /**/
                        Назва = cur.Fields[Блокнот_Const.Назва].ToString() ?? "" /**/
                        
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
        string Image { get { return AppContext.BaseDirectory + "images/" + (DeletionLabel ? "doc_delete.png" : "doc.png"); } }
        bool DeletionLabel = false;
        string ID = "";
        
        string Код = "";
        string Назва = "";

        Array ToArray()
        {
            return new object[] { new Gdk.Pixbuf(Image), ID,
            Код, Назва,  };
        }

        public static ListStore Store = new ListStore([/*Image*/ typeof(Gdk.Pixbuf), /*ID*/ typeof(string),
            /*Код*/ typeof(string), /*Назва*/ typeof(string), ]);

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

        public static List<Where> Where { get; set; } = [];

        public static UnigueID? DirectoryPointerItem { get; set; }
        public static UnigueID? SelectPointerItem { get; set; }
        public static TreePath? FirstPath;
        public static TreePath? SelectPath;
        public static TreePath? CurrentPath;

        public static async ValueTask LoadRecords()
        {
            Store.Clear();
            FirstPath = SelectPath = CurrentPath = null;

            Довідники.Блокнот_Select Блокнот_Select = new Довідники.Блокнот_Select();
            Блокнот_Select.QuerySelect.Field.AddRange(
                ["deletion_label",
                  /*Код*/ Довідники.Блокнот_Const.Код,
                  /*Назва*/ Довідники.Блокнот_Const.Назва,
                  
                ]);

            /* Where */
            Блокнот_Select.QuerySelect.Where = Where;

            

            /* SELECT */
            await Блокнот_Select.Select();
            while (Блокнот_Select.MoveNext())
            {
                Довідники.Блокнот_Pointer? cur = Блокнот_Select.Current;

                if (cur != null)
                {
                    Блокнот_ЗаписиШвидкийВибір Record = new Блокнот_ЗаписиШвидкийВибір
                    {
                        ID = cur.UnigueID.ToString(),
                        DeletionLabel = (bool)cur.Fields?["deletion_label"]!, /*Помітка на видалення*/
                        Код = cur.Fields[Блокнот_Const.Код].ToString() ?? "", /**/
                        Назва = cur.Fields[Блокнот_Const.Назва].ToString() ?? "" /**/
                        
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

    

    //
    // Журнали
    //

    
    #region JOURNAL "Повний"
    
    public class Журнали_Повний
    {
        string Image { get { return AppContext.BaseDirectory + "images/" + (DeletionLabel ? "doc_delete.png" : "doc.png"); }}
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
            return new object[] { new Gdk.Pixbuf(Image), ID, Type, /*Проведений документ*/ Spend,
            /*Назва*/ Назва, /*Дата*/ Дата, /*Номер*/ Номер, /*Коментар*/ Коментар,  };
        }

        // Джерело даних для списку
        public static ListStore Store = new ListStore([
          typeof(Gdk.Pixbuf), /* Image */
          typeof(string), /* ID */
          typeof(string), /* Type */
          typeof(bool), /* Spend Проведений документ */
          typeof(string), /*Назва*/
          typeof(string), /*Дата*/
          typeof(string), /*Номер*/
          typeof(string), /*Коментар*/
          ]);

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
            
        }

        // Список документів які входять в журнал
        public static Dictionary<string, string> AllowDocument()
        {
            Dictionary<string, string> allowDoc = new Dictionary<string, string>()
            {
                
            };
            return allowDoc;
        }

        public static UnigueID? SelectPointerItem { get; set; }
        public static TreePath? SelectPath;
        public static TreePath? CurrentPath;

        // Завантаження даних
        public static async ValueTask LoadRecords() 
        {
            Store.Clear();
            SelectPath = CurrentPath = null;
            List<string> allQuery = new List<string>();
            Dictionary<string, object> paramQuery = new Dictionary<string, object>();

          
        }
    }
    #endregion
    
}

namespace StorageAndTrade_1_0.РегістриВідомостей.ТабличніСписки
{
    
}

  