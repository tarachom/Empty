
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
  
 * Дата конфігурації: 15.01.2024 19:10:10
 *
 *
 * Цей код згенерований в Конфігураторі 3. Шаблон Gtk.xslt
 *
 */

using Gtk;
using AccountingSoftware;

namespace StorageAndTrade_1_0.Іконки
{
    public static class ДляТабличногоСписку
    {
        public static Gdk.Pixbuf Normal = new Gdk.Pixbuf($"{AppContext.BaseDirectory}images/doc.png");
        public static Gdk.Pixbuf Delete = new Gdk.Pixbuf($"{AppContext.BaseDirectory}images/doc_delete.png");
    }

    public static class ДляДерева
    {
        public static Gdk.Pixbuf Normal = new Gdk.Pixbuf($"{AppContext.BaseDirectory}images/folder.png");
        public static Gdk.Pixbuf Delete = new Gdk.Pixbuf($"{AppContext.BaseDirectory}images/folder_delete.png");
    }
}

namespace StorageAndTrade_1_0
{
    public abstract class ТабличнийСписок
    {
        public static void ДодатиВідбір(TreeView treeView, Where where, bool clear_all_before_add = false)
        {
            if (!treeView.Data.ContainsKey("Where"))
                treeView.Data.Add("Where", new List<Where>() { where });
            else
            {
                if (clear_all_before_add)
                    treeView.Data["Where"] = new List<Where>() { where };
                else
                {
                    object? value = treeView.Data["Where"];
                    if (value == null)
                        treeView.Data["Where"] = new List<Where>() { where };
                    else
                        ((List<Where>)value).Add(where);
                }
            }
        }

        public static void ДодатиВідбір(TreeView treeView, List<Where> where, bool clear_all_before_add = false)
        {
            if (!treeView.Data.ContainsKey("Where"))
                treeView.Data.Add("Where", where);
            else
            {
                if (clear_all_before_add)
                    treeView.Data["Where"] = where;
                else
                {
                    object? value = treeView.Data["Where"];
                    if (value == null)
                        treeView.Data["Where"] = where;
                    else
                    {
                        var list = (List<Where>)value;
                        foreach (Where item in where)
                            list.Add(item);
                    }
                }
            }
        }

        public static void ОчиститиВідбір(TreeView treeView)
        {
            if (treeView.Data.ContainsKey("Where"))
                treeView.Data["Where"] = null;
        }
    }
}

namespace StorageAndTrade_1_0.Довідники.ТабличніСписки
{
    
    #region DIRECTORY "Користувачі"
    
      
    /* ТАБЛИЦЯ */
    public class Користувачі_Записи : ТабличнийСписок
    {
        bool DeletionLabel = false;
        string ID = "";
        
        string Код = "";
        string Назва = "";

        Array ToArray()
        {
            return new object[] 
            { 
                DeletionLabel ? Іконки.ДляТабличногоСписку.Delete : Іконки.ДляТабличногоСписку.Normal,
                ID,
                /*Код*/ Код,
                /*Назва*/ Назва,
                
            };
        }

        public static void AddColumns(TreeView treeView)
        {
            treeView.Model = new ListStore(
            [
                /*Image*/ typeof(Gdk.Pixbuf), 
                /*ID*/ typeof(string),
                /*Код*/ typeof(string),  
                /*Назва*/ typeof(string),  
                
            ]);

            treeView.AppendColumn(new TreeViewColumn("", new CellRendererPixbuf(), "pixbuf", 0)); /* { Ypad = 4 } */
            treeView.AppendColumn(new TreeViewColumn("ID", new CellRendererText(), "text", 1) { Visible = false });
            /* */
            treeView.AppendColumn(new TreeViewColumn("Код", new CellRendererText() { Xpad = 4 }, "text", 2) { MinWidth = 20, Resizable = true, SortColumnId = 2 } ); /*Код*/
            treeView.AppendColumn(new TreeViewColumn("Назва", new CellRendererText() { Xpad = 4 }, "text", 3) { MinWidth = 20, Resizable = true, SortColumnId = 3 } ); /*Назва*/
            
            //Пустишка
            treeView.AppendColumn(new TreeViewColumn());
        }

        public static UnigueID? DirectoryPointerItem { get; set; }
        public static UnigueID? SelectPointerItem { get; set; }
        public static TreePath? FirstPath;
        public static TreePath? SelectPath;
        public static TreePath? CurrentPath;

        public static async ValueTask LoadRecords(TreeView treeView)
        {
            FirstPath = SelectPath = CurrentPath = null;

            Довідники.Користувачі_Select Користувачі_Select = new Довідники.Користувачі_Select();
            Користувачі_Select.QuerySelect.Field.AddRange(
            [
                /*Помітка на видалення*/ "deletion_label",
                /*Код*/ Довідники.Користувачі_Const.Код,
                /*Назва*/ Довідники.Користувачі_Const.Назва,
                
            ]);

            /* Where */
            if (treeView.Data.ContainsKey("Where"))
            {
                var where = treeView.Data["Where"];
                if (where != null) Користувачі_Select.QuerySelect.Where = (List<Where>)where;
            }

            
              /* ORDER */
              Користувачі_Select.QuerySelect.Order.Add(Довідники.Користувачі_Const.Назва, SelectOrder.ASC);
            

            /* SELECT */
            await Користувачі_Select.Select();

            ListStore Store = (ListStore)treeView.Model;
            Store.Clear();

            while (Користувачі_Select.MoveNext())
            {
                Довідники.Користувачі_Pointer? cur = Користувачі_Select.Current;

                if (cur != null)
                {
                    Dictionary<string, object> Fields = cur.Fields!;
                    Користувачі_Записи Record = new Користувачі_Записи
                    {
                        ID = cur.UnigueID.ToString(),
                        DeletionLabel = (bool)Fields["deletion_label"], /*Помітка на видалення*/
                        Код = Fields[Користувачі_Const.Код].ToString() ?? "", /**/
                        Назва = Fields[Користувачі_Const.Назва].ToString() ?? "" /**/
                        
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
    public class Користувачі_ЗаписиШвидкийВибір : ТабличнийСписок
    {
        bool DeletionLabel = false;
        string ID = "";
        
        string Код = "";
        string Назва = "";

        Array ToArray()
        {
            return new object[] 
            { 
                DeletionLabel ? Іконки.ДляТабличногоСписку.Delete : Іконки.ДляТабличногоСписку.Normal,
                ID,
                /*Код*/ Код,
                /*Назва*/ Назва,
                
            };
        }

        public static void AddColumns(TreeView treeView)
        {
            treeView.Model = new ListStore(
            [
                /*Image*/ typeof(Gdk.Pixbuf), 
                /*ID*/ typeof(string),
                /*Код*/ typeof(string),  
                /*Назва*/ typeof(string),  
                
            ]);

            treeView.AppendColumn(new TreeViewColumn("", new CellRendererPixbuf(), "pixbuf", 0)); /* { Ypad = 4 } */
            treeView.AppendColumn(new TreeViewColumn("ID", new CellRendererText(), "text", 1) { Visible = false });
            /* */
            treeView.AppendColumn(new TreeViewColumn("Код", new CellRendererText() { Xpad = 4 }, "text", 2) { MinWidth = 20, Resizable = true, SortColumnId = 2 } ); /*Код*/
            treeView.AppendColumn(new TreeViewColumn("Назва", new CellRendererText() { Xpad = 4 }, "text", 3) { MinWidth = 20, Resizable = true, SortColumnId = 3 } ); /*Назва*/
            
            //Пустишка
            treeView.AppendColumn(new TreeViewColumn());
        }

        public static UnigueID? DirectoryPointerItem { get; set; }
        public static UnigueID? SelectPointerItem { get; set; }
        public static TreePath? FirstPath;
        public static TreePath? SelectPath;
        public static TreePath? CurrentPath;

        public static async ValueTask LoadRecords(TreeView treeView)
        {
            FirstPath = SelectPath = CurrentPath = null;

            Довідники.Користувачі_Select Користувачі_Select = new Довідники.Користувачі_Select();
            Користувачі_Select.QuerySelect.Field.AddRange(
            [
                /*Помітка на видалення*/ "deletion_label",
                /*Код*/ Довідники.Користувачі_Const.Код,
                /*Назва*/ Довідники.Користувачі_Const.Назва,
                
            ]);

            /* Where */
            if (treeView.Data.ContainsKey("Where"))
            {
                var where = treeView.Data["Where"];
                if (where != null) Користувачі_Select.QuerySelect.Where = (List<Where>)where;
            }

            
              /* ORDER */
              Користувачі_Select.QuerySelect.Order.Add(Довідники.Користувачі_Const.Назва, SelectOrder.ASC);
            

            /* SELECT */
            await Користувачі_Select.Select();

            ListStore Store = (ListStore)treeView.Model;
            Store.Clear();

            while (Користувачі_Select.MoveNext())
            {
                Довідники.Користувачі_Pointer? cur = Користувачі_Select.Current;

                if (cur != null)
                {
                    Dictionary<string, object> Fields = cur.Fields!;
                    Користувачі_ЗаписиШвидкийВибір Record = new Користувачі_ЗаписиШвидкийВибір
                    {
                        ID = cur.UnigueID.ToString(),
                        DeletionLabel = (bool)Fields["deletion_label"], /*Помітка на видалення*/
                        Код = Fields[Користувачі_Const.Код].ToString() ?? "", /**/
                        Назва = Fields[Користувачі_Const.Назва].ToString() ?? "" /**/
                        
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
    public class Блокнот_Записи : ТабличнийСписок
    {
        bool DeletionLabel = false;
        string ID = "";
        
        string Код = "";
        string Назва = "";

        Array ToArray()
        {
            return new object[] 
            { 
                DeletionLabel ? Іконки.ДляТабличногоСписку.Delete : Іконки.ДляТабличногоСписку.Normal,
                ID,
                /*Код*/ Код,
                /*Назва*/ Назва,
                
            };
        }

        public static void AddColumns(TreeView treeView)
        {
            treeView.Model = new ListStore(
            [
                /*Image*/ typeof(Gdk.Pixbuf), 
                /*ID*/ typeof(string),
                /*Код*/ typeof(string),  
                /*Назва*/ typeof(string),  
                
            ]);

            treeView.AppendColumn(new TreeViewColumn("", new CellRendererPixbuf(), "pixbuf", 0)); /* { Ypad = 4 } */
            treeView.AppendColumn(new TreeViewColumn("ID", new CellRendererText(), "text", 1) { Visible = false });
            /* */
            treeView.AppendColumn(new TreeViewColumn("Код", new CellRendererText() { Xpad = 4 }, "text", 2) { MinWidth = 20, Resizable = true, SortColumnId = 2 } ); /*Код*/
            treeView.AppendColumn(new TreeViewColumn("Назва", new CellRendererText() { Xpad = 4 }, "text", 3) { MinWidth = 20, Resizable = true, SortColumnId = 3 } ); /*Назва*/
            
            //Пустишка
            treeView.AppendColumn(new TreeViewColumn());
        }

        public static UnigueID? DirectoryPointerItem { get; set; }
        public static UnigueID? SelectPointerItem { get; set; }
        public static TreePath? FirstPath;
        public static TreePath? SelectPath;
        public static TreePath? CurrentPath;

        public static async ValueTask LoadRecords(TreeView treeView)
        {
            FirstPath = SelectPath = CurrentPath = null;

            Довідники.Блокнот_Select Блокнот_Select = new Довідники.Блокнот_Select();
            Блокнот_Select.QuerySelect.Field.AddRange(
            [
                /*Помітка на видалення*/ "deletion_label",
                /*Код*/ Довідники.Блокнот_Const.Код,
                /*Назва*/ Довідники.Блокнот_Const.Назва,
                
            ]);

            /* Where */
            if (treeView.Data.ContainsKey("Where"))
            {
                var where = treeView.Data["Where"];
                if (where != null) Блокнот_Select.QuerySelect.Where = (List<Where>)where;
            }

            
              /* ORDER */
              Блокнот_Select.QuerySelect.Order.Add(Довідники.Блокнот_Const.Код, SelectOrder.ASC);
            

            /* SELECT */
            await Блокнот_Select.Select();

            ListStore Store = (ListStore)treeView.Model;
            Store.Clear();

            while (Блокнот_Select.MoveNext())
            {
                Довідники.Блокнот_Pointer? cur = Блокнот_Select.Current;

                if (cur != null)
                {
                    Dictionary<string, object> Fields = cur.Fields!;
                    Блокнот_Записи Record = new Блокнот_Записи
                    {
                        ID = cur.UnigueID.ToString(),
                        DeletionLabel = (bool)Fields["deletion_label"], /*Помітка на видалення*/
                        Код = Fields[Блокнот_Const.Код].ToString() ?? "", /**/
                        Назва = Fields[Блокнот_Const.Назва].ToString() ?? "" /**/
                        
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
    public class Блокнот_ЗаписиШвидкийВибір : ТабличнийСписок
    {
        bool DeletionLabel = false;
        string ID = "";
        
        string Код = "";
        string Назва = "";

        Array ToArray()
        {
            return new object[] 
            { 
                DeletionLabel ? Іконки.ДляТабличногоСписку.Delete : Іконки.ДляТабличногоСписку.Normal,
                ID,
                /*Код*/ Код,
                /*Назва*/ Назва,
                
            };
        }

        public static void AddColumns(TreeView treeView)
        {
            treeView.Model = new ListStore(
            [
                /*Image*/ typeof(Gdk.Pixbuf), 
                /*ID*/ typeof(string),
                /*Код*/ typeof(string),  
                /*Назва*/ typeof(string),  
                
            ]);

            treeView.AppendColumn(new TreeViewColumn("", new CellRendererPixbuf(), "pixbuf", 0)); /* { Ypad = 4 } */
            treeView.AppendColumn(new TreeViewColumn("ID", new CellRendererText(), "text", 1) { Visible = false });
            /* */
            treeView.AppendColumn(new TreeViewColumn("Код", new CellRendererText() { Xpad = 4 }, "text", 2) { MinWidth = 20, Resizable = true, SortColumnId = 2 } ); /*Код*/
            treeView.AppendColumn(new TreeViewColumn("Назва", new CellRendererText() { Xpad = 4 }, "text", 3) { MinWidth = 20, Resizable = true, SortColumnId = 3 } ); /*Назва*/
            
            //Пустишка
            treeView.AppendColumn(new TreeViewColumn());
        }

        public static UnigueID? DirectoryPointerItem { get; set; }
        public static UnigueID? SelectPointerItem { get; set; }
        public static TreePath? FirstPath;
        public static TreePath? SelectPath;
        public static TreePath? CurrentPath;

        public static async ValueTask LoadRecords(TreeView treeView)
        {
            FirstPath = SelectPath = CurrentPath = null;

            Довідники.Блокнот_Select Блокнот_Select = new Довідники.Блокнот_Select();
            Блокнот_Select.QuerySelect.Field.AddRange(
            [
                /*Помітка на видалення*/ "deletion_label",
                /*Код*/ Довідники.Блокнот_Const.Код,
                /*Назва*/ Довідники.Блокнот_Const.Назва,
                
            ]);

            /* Where */
            if (treeView.Data.ContainsKey("Where"))
            {
                var where = treeView.Data["Where"];
                if (where != null) Блокнот_Select.QuerySelect.Where = (List<Where>)where;
            }

            

            /* SELECT */
            await Блокнот_Select.Select();

            ListStore Store = (ListStore)treeView.Model;
            Store.Clear();

            while (Блокнот_Select.MoveNext())
            {
                Довідники.Блокнот_Pointer? cur = Блокнот_Select.Current;

                if (cur != null)
                {
                    Dictionary<string, object> Fields = cur.Fields!;
                    Блокнот_ЗаписиШвидкийВибір Record = new Блокнот_ЗаписиШвидкийВибір
                    {
                        ID = cur.UnigueID.ToString(),
                        DeletionLabel = (bool)Fields["deletion_label"], /*Помітка на видалення*/
                        Код = Fields[Блокнот_Const.Код].ToString() ?? "", /**/
                        Назва = Fields[Блокнот_Const.Назва].ToString() ?? "" /**/
                        
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

        public static Where? ВідбірПоПеріоду(string fieldWhere, Перелічення.ТипПеріодуДляЖурналівДокументів типПеріоду)
        {
            switch (типПеріоду)
            {
                case Перелічення.ТипПеріодуДляЖурналівДокументів.ЗПочаткуРоку:
                    return new Where(fieldWhere, Comparison.QT_EQ, new DateTime(DateTime.Now.Year, 1, 1));
                case Перелічення.ТипПеріодуДляЖурналівДокументів.Квартал:
                {
                    DateTime ДатаТриМісцяНазад = DateTime.Now.AddMonths(-3);
                    return new Where(fieldWhere, Comparison.QT_EQ, new DateTime(ДатаТриМісцяНазад.Year, ДатаТриМісцяНазад.Month, 1));
                }
                case Перелічення.ТипПеріодуДляЖурналівДокументів.ЗМинулогоМісяця:
                {
                    DateTime ДатаМісцьНазад = DateTime.Now.AddMonths(-1);
                    return new Where(fieldWhere, Comparison.QT_EQ, new DateTime(ДатаМісцьНазад.Year, ДатаМісцьНазад.Month, 1));
                }
                case Перелічення.ТипПеріодуДляЖурналівДокументів.Місяць:
                    return new Where(fieldWhere, Comparison.QT_EQ, DateTime.Now.AddMonths(-1));
                case Перелічення.ТипПеріодуДляЖурналівДокументів.ЗПочаткуМісяця:
                    return new Where(fieldWhere, Comparison.QT_EQ, new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));
                case Перелічення.ТипПеріодуДляЖурналівДокументів.ЗПочаткуТижня:
                {
                    DateTime СімДнівНазад = DateTime.Now.AddDays(-7);
                    return new Where(fieldWhere, Comparison.QT_EQ, new DateTime(СімДнівНазад.Year, СімДнівНазад.Month, СімДнівНазад.Day));
                }
                case Перелічення.ТипПеріодуДляЖурналівДокументів.ПоточнийДень:
                    return new Where(fieldWhere, Comparison.QT_EQ, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
                default: 
                    return null;
            }
        }
    }

    

    //
    // Журнали
    //

    
    #region JOURNAL "Повний"
    
    public class Журнали_Повний
    {
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
            return new object[] 
            { 
                DeletionLabel ? Іконки.ДляТабличногоСписку.Delete : Іконки.ДляТабличногоСписку.Normal, 
                ID, 
                Type, 
                /*Проведений документ*/ Spend,
                /*Назва*/ Назва, /*Дата*/ Дата, /*Номер*/ Номер, /*Коментар*/ Коментар,  
            };
        }

        // Добавлення колонок в список
        public static void AddColumns(TreeView treeView)
        {
            treeView.Model = new ListStore(
            [
                typeof(Gdk.Pixbuf), /* Image */
                typeof(string), /* ID */
                typeof(string), /* Type */
                typeof(bool), /* Spend Проведений документ */
                typeof(string), /*Назва*/
                typeof(string), /*Дата*/
                typeof(string), /*Номер*/
                typeof(string), /*Коментар*/
                
            ]);

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

        public static void ДодатиВідбірПоПеріоду(TreeView treeView, Перелічення.ТипПеріодуДляЖурналівДокументів типПеріоду)
        {
            Dictionary<string, List<Where>> WhereDict = [];
            if (!treeView.Data.ContainsKey("Where"))
                treeView.Data.Add("Where", WhereDict);
            else
                treeView.Data["Where"] = WhereDict;
            
        }

        public static void ОчиститиВідбір(TreeView treeView)
        {
            if (treeView.Data.ContainsKey("Where"))
                treeView.Data["Where"] = null;
        }

        // Список документів які входять в журнал
        public static Dictionary<string, string> AllowDocument()
        {
            return new Dictionary<string, string>()
            {
                
            };
        }

        public static UnigueID? SelectPointerItem { get; set; }
        public static TreePath? SelectPath;
        public static TreePath? CurrentPath;

        // Завантаження даних
        public static async ValueTask LoadRecords(TreeView treeView) 
        {
            SelectPath = CurrentPath = null;

            List<string> allQuery = [];
            Dictionary<string, object> paramQuery = [];

          
        }
    }
    #endregion
    
}

namespace StorageAndTrade_1_0.РегістриВідомостей.ТабличніСписки
{
    
}

  