
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
  
 * Дата конфігурації: 12.05.2023 19:41:32
 *
 *
 * Цей код згенерований в Конфігураторі 3. Шаблон CodeGeneration.xslt
 *
 */

using AccountingSoftware;
using System.Xml;

namespace StorageAndTrade_1_0
{
    public static class Config
    {
        public static Kernel? Kernel { get; set; }
		
        public static void ReadAllConstants()
        {
            Константи.Системні.ReadAll();
            Константи.ЖурналиДокументів.ReadAll();
            Константи.ПриЗапускуПрограми.ReadAll();
            Константи.НумераціяДовідників.ReadAll();
            Константи.НумераціяДокументів.ReadAll();
            
        }
    }

    public class Functions
    {
        /*
          Функція для типу який задається користувачем.
          Повертає презентацію для uuidAndText.
          В @pointer - повертає групу (Документи або Довідники)
            @type - повертає назву типу
        */
        public static string CompositePointerPresentation(UuidAndText uuidAndText, out string pointer, out string type)
        {
            pointer = type = "";

            if (uuidAndText.IsEmpty() || String.IsNullOrEmpty(uuidAndText.Text) || uuidAndText.Text.IndexOf(".") == -1)
                return "";

            string[] pointer_and_type = uuidAndText.Text.Split(".", StringSplitOptions.None);

            if (pointer_and_type.Length == 2)
            {
                pointer = pointer_and_type[0];
                type = pointer_and_type[1];

                if (pointer == "Документи")
                {
                    
                    switch (type)
                    {
                        
                        case "Подія": return new Документи.Подія_Pointer(uuidAndText.Uuid).GetPresentation();
                        
                    }
                    
                }
                else if (pointer == "Довідники")
                {
                    
                    switch (type)
                    {
                        
                        case "Користувачі": return new Довідники.Користувачі_Pointer(uuidAndText.Uuid).GetPresentation();
                        
                        case "Блокнот": return new Довідники.Блокнот_Pointer(uuidAndText.Uuid).GetPresentation();
                        
                    }
                    
                }
            }

            return "";
        }
    }
}

namespace StorageAndTrade_1_0.Константи
{
    
	  #region CONSTANTS BLOCK "Системні"
    public static class Системні
    {
        public static void ReadAll()
        {
            
            Dictionary<string, object> fieldValue = new Dictionary<string, object>();
            bool IsSelect = Config.Kernel!.DataBase.SelectAllConstants("tab_constants",
                 new string[] { "col_a2", "col_a9" }, fieldValue);
            
            if (IsSelect)
            {
                m_ЗупинитиФоновіЗадачі_Const = (fieldValue["col_a2"] != DBNull.Value) ? (bool)fieldValue["col_a2"] : false;
                m_ПовідомленняТаПомилки_Const = fieldValue["col_a9"].ToString() ?? "";
                
            }
			      
        }
        
        
        static bool m_ЗупинитиФоновіЗадачі_Const = false;
        public static bool ЗупинитиФоновіЗадачі_Const
        {
            get 
            {
                return m_ЗупинитиФоновіЗадачі_Const;
            }
            set
            {
                m_ЗупинитиФоновіЗадачі_Const = value;
                Config.Kernel!.DataBase.SaveConstants("tab_constants", "col_a2", m_ЗупинитиФоновіЗадачі_Const);
            }
        }
        
        static string m_ПовідомленняТаПомилки_Const = "";
        public static string ПовідомленняТаПомилки_Const
        {
            get 
            {
                return m_ПовідомленняТаПомилки_Const;
            }
            set
            {
                m_ПовідомленняТаПомилки_Const = value;
                Config.Kernel!.DataBase.SaveConstants("tab_constants", "col_a9", m_ПовідомленняТаПомилки_Const);
            }
        }
        
        
        public class ПовідомленняТаПомилки_Помилки_TablePart : ConstantsTablePart
        {
            public ПовідомленняТаПомилки_Помилки_TablePart() : base(Config.Kernel!, "tab_a02",
                 new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5", "col_a6" }) 
            {
                Records = new List<Record>();
            }
            
            public const string TABLE = "tab_a02";
            
            public const string Дата = "col_a1";
            public const string НазваПроцесу = "col_a2";
            public const string Обєкт = "col_a3";
            public const string ТипОбєкту = "col_a4";
            public const string НазваОбєкту = "col_a5";
            public const string Повідомлення = "col_a6";
            public List<Record> Records { get; set; }
        
            public void Read()
            {
                Records.Clear();
                base.BaseRead();

                foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
                {
                    Record record = new Record();
                    record.UID = (Guid)fieldValue["uid"];
                    
                    record.Дата = (fieldValue["col_a1"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a1"].ToString() ?? DateTime.MinValue.ToString()) : DateTime.MinValue;
                    record.НазваПроцесу = fieldValue["col_a2"].ToString() ?? "";
                    record.Обєкт = (fieldValue["col_a3"] != DBNull.Value) ? (Guid)fieldValue["col_a3"] : Guid.Empty;
                    record.ТипОбєкту = fieldValue["col_a4"].ToString() ?? "";
                    record.НазваОбєкту = fieldValue["col_a5"].ToString() ?? "";
                    record.Повідомлення = fieldValue["col_a6"].ToString() ?? "";
                    
                    Records.Add(record);
                }
            
                base.BaseClear();
            }
        
            public void Save(bool clear_all_before_save /*= true*/) 
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete();

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a1", record.Дата);
                    fieldValue.Add("col_a2", record.НазваПроцесу);
                    fieldValue.Add("col_a3", record.Обєкт);
                    fieldValue.Add("col_a4", record.ТипОбєкту);
                    fieldValue.Add("col_a5", record.НазваОбєкту);
                    fieldValue.Add("col_a6", record.Повідомлення);
                    
                    record.UID = base.BaseSave(record.UID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        
            public void Delete()
            {
                base.BaseDelete();
            }
            
            public class Record : ConstantsTablePartRecord
            {
                public DateTime Дата { get; set; } = DateTime.MinValue;
                public string НазваПроцесу { get; set; } = "";
                public Guid Обєкт { get; set; } = new Guid();
                public string ТипОбєкту { get; set; } = "";
                public string НазваОбєкту { get; set; } = "";
                public string Повідомлення { get; set; } = "";
                
            }
        }
               
    }
    #endregion
    
	  #region CONSTANTS BLOCK "ЖурналиДокументів"
    public static class ЖурналиДокументів
    {
        public static void ReadAll()
        {
            
            Dictionary<string, object> fieldValue = new Dictionary<string, object>();
            bool IsSelect = Config.Kernel!.DataBase.SelectAllConstants("tab_constants",
                 new string[] { "col_a8" }, fieldValue);
            
            if (IsSelect)
            {
                m_ОсновнийТипПеріоду_Const = (fieldValue["col_a8"] != DBNull.Value) ? (Перелічення.ТипПеріодуДляЖурналівДокументів)fieldValue["col_a8"] : 0;
                
            }
			      
        }
        
        
        static Перелічення.ТипПеріодуДляЖурналівДокументів m_ОсновнийТипПеріоду_Const = 0;
        public static Перелічення.ТипПеріодуДляЖурналівДокументів ОсновнийТипПеріоду_Const
        {
            get 
            {
                return m_ОсновнийТипПеріоду_Const;
            }
            set
            {
                m_ОсновнийТипПеріоду_Const = value;
                Config.Kernel!.DataBase.SaveConstants("tab_constants", "col_a8", (int)m_ОсновнийТипПеріоду_Const);
            }
        }
             
    }
    #endregion
    
	  #region CONSTANTS BLOCK "ПриЗапускуПрограми"
    public static class ПриЗапускуПрограми
    {
        public static void ReadAll()
        {
            
        }
        
             
    }
    #endregion
    
	  #region CONSTANTS BLOCK "НумераціяДовідників"
    public static class НумераціяДовідників
    {
        public static void ReadAll()
        {
            
            Dictionary<string, object> fieldValue = new Dictionary<string, object>();
            bool IsSelect = Config.Kernel!.DataBase.SelectAllConstants("tab_constants",
                 new string[] { "col_a1", "col_a3" }, fieldValue);
            
            if (IsSelect)
            {
                m_Користувачі_Const = (fieldValue["col_a1"] != DBNull.Value) ? (int)fieldValue["col_a1"] : 0;
                m_Блокнот_Const = (fieldValue["col_a3"] != DBNull.Value) ? (int)fieldValue["col_a3"] : 0;
                
            }
			      
        }
        
        
        static int m_Користувачі_Const = 0;
        public static int Користувачі_Const
        {
            get 
            {
                return m_Користувачі_Const;
            }
            set
            {
                m_Користувачі_Const = value;
                Config.Kernel!.DataBase.SaveConstants("tab_constants", "col_a1", m_Користувачі_Const);
            }
        }
        
        static int m_Блокнот_Const = 0;
        public static int Блокнот_Const
        {
            get 
            {
                return m_Блокнот_Const;
            }
            set
            {
                m_Блокнот_Const = value;
                Config.Kernel!.DataBase.SaveConstants("tab_constants", "col_a3", m_Блокнот_Const);
            }
        }
             
    }
    #endregion
    
	  #region CONSTANTS BLOCK "НумераціяДокументів"
    public static class НумераціяДокументів
    {
        public static void ReadAll()
        {
            
            Dictionary<string, object> fieldValue = new Dictionary<string, object>();
            bool IsSelect = Config.Kernel!.DataBase.SelectAllConstants("tab_constants",
                 new string[] { "col_a4" }, fieldValue);
            
            if (IsSelect)
            {
                m_Подія_Const = (fieldValue["col_a4"] != DBNull.Value) ? (int)fieldValue["col_a4"] : 0;
                
            }
			      
        }
        
        
        static int m_Подія_Const = 0;
        public static int Подія_Const
        {
            get 
            {
                return m_Подія_Const;
            }
            set
            {
                m_Подія_Const = value;
                Config.Kernel!.DataBase.SaveConstants("tab_constants", "col_a4", m_Подія_Const);
            }
        }
             
    }
    #endregion
    
}

namespace StorageAndTrade_1_0.Довідники
{
    
    #region DIRECTORY "Користувачі"
    public static class Користувачі_Const
    {
        public const string FULLNAME = "Користувачі";
        public const string TABLE = "tab_a08";
        public const string DELETION_LABEL = "deletion_label";
        
        public const string Код = "col_a1";
        public const string Назва = "col_a2";
        public const string КодВСпеціальнійТаблиці = "col_a3";
        public const string Коментар = "col_a4";
        public const string Заблокований = "col_a5";
    }

    public class Користувачі_Objest : DirectoryObject
    {
        public Користувачі_Objest() : base(Config.Kernel!, "tab_a08",
             new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5" }) 
        {
            Код = "";
            Назва = "";
            КодВСпеціальнійТаблиці = new Guid();
            Коментар = "";
            Заблокований = false;
            
        }
        
        public void New()
        {
            BaseNew();
            Користувачі_Triggers.New(this);
            
        }

        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Код = base.FieldValue["col_a1"].ToString() ?? "";
                Назва = base.FieldValue["col_a2"].ToString() ?? "";
                КодВСпеціальнійТаблиці = (base.FieldValue["col_a3"] != DBNull.Value) ? (Guid)base.FieldValue["col_a3"] : Guid.Empty;
                Коментар = base.FieldValue["col_a4"].ToString() ?? "";
                Заблокований = (base.FieldValue["col_a5"] != DBNull.Value) ? (bool)base.FieldValue["col_a5"] : false;
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public bool Save()
        {
            Користувачі_Triggers.BeforeSave(this);
            base.FieldValue["col_a1"] = Код;
            base.FieldValue["col_a2"] = Назва;
            base.FieldValue["col_a3"] = КодВСпеціальнійТаблиці;
            base.FieldValue["col_a4"] = Коментар;
            base.FieldValue["col_a5"] = Заблокований;
            
            bool result = BaseSave();
            if (result)
            {
                Користувачі_Triggers.AfterSave(this);
                BaseWriteFullTextSearch(GetBasis(), new string[] {  });
            }
            return result;
        }

        public Користувачі_Objest Copy(bool copyTableParts = false)
        {
            Користувачі_Objest copy = new Користувачі_Objest();
            copy.Код = Код;
            copy.Назва = Назва;
            copy.КодВСпеціальнійТаблиці = КодВСпеціальнійТаблиці;
            copy.Коментар = Коментар;
            copy.Заблокований = Заблокований;
            
            
            if (copyTableParts)
            {
            
            }

            copy.New();
            Користувачі_Triggers.Copying(copy, this);
            return copy;
        }

        public void SetDeletionLabel(bool label = true)
        {
            Користувачі_Triggers.SetDeletionLabel(this, label);
            base.BaseDeletionLabel(label);
        }

        public void Delete()
        {
            Користувачі_Triggers.BeforeDelete(this);
            base.BaseDelete(new string[] {  });
        }
        
        public Користувачі_Pointer GetDirectoryPointer()
        {
            return new Користувачі_Pointer(UnigueID.UGuid);
        }

        public UuidAndText GetBasis()
        {
            return new UuidAndText(UnigueID.UGuid, "Довідники.Користувачі");
        }

        public string GetPresentation()
        {
            return base.BasePresentation(
                new string[] { "col_a2" }
            );
        }
        
        public string Код { get; set; }
        public string Назва { get; set; }
        public Guid КодВСпеціальнійТаблиці { get; set; }
        public string Коментар { get; set; }
        public bool Заблокований { get; set; }
        
    }

    public class Користувачі_Pointer : DirectoryPointer
    {
        public Користувачі_Pointer(object? uid = null) : base(Config.Kernel!, "tab_a08")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Користувачі_Pointer(UnigueID uid, Dictionary<string, object>? fields = null) : base(Config.Kernel!, "tab_a08")
        {
            base.Init(uid, fields);
        }
        
        public Користувачі_Objest? GetDirectoryObject()
        {
            if (this.IsEmpty()) return null;
            Користувачі_Objest КористувачіObjestItem = new Користувачі_Objest();
            return КористувачіObjestItem.Read(base.UnigueID) ? КористувачіObjestItem : null;
        }

        public Користувачі_Pointer Copy()
        {
            return new Користувачі_Pointer(base.UnigueID, base.Fields) { Назва = Назва };
        }

        public string Назва { get; set; } = "";

        public string GetPresentation()
        {
            return Назва = base.BasePresentation(
                new string[] { "col_a2" }
            );
        }

        public void SetDeletionLabel(bool label = true)
        {
            Користувачі_Objest? obj = GetDirectoryObject();
            if (obj != null)
            {
                Користувачі_Triggers.SetDeletionLabel(obj, label);
                
                base.BaseDeletionLabel(label);
            }
        }
		
        public Користувачі_Pointer GetEmptyPointer()
        {
            return new Користувачі_Pointer();
        }

        public UuidAndText GetBasis()
        {
            return new UuidAndText(UnigueID.UGuid, "Довідники.Користувачі");
        }

        public void Clear()
        {
            Init(new UnigueID(), null);
            Назва = "";
        }
    }
    
    public class Користувачі_Select : DirectorySelect
    {
        public Користувачі_Select() : base(Config.Kernel!, "tab_a08") { }        
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Користувачі_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Користувачі_Pointer? Current { get; private set; }
        
        public Користувачі_Pointer FindByField(string name, object value)
        {
            Користувачі_Pointer itemPointer = new Користувачі_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(name, value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Користувачі_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Користувачі_Pointer> directoryPointerList = new List<Користувачі_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(name, value, limit, offset)) 
                directoryPointerList.Add(new Користувачі_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
   
    #endregion
    
    #region DIRECTORY "Блокнот"
    public static class Блокнот_Const
    {
        public const string FULLNAME = "Блокнот";
        public const string TABLE = "tab_a01";
        public const string DELETION_LABEL = "deletion_label";
        
        public const string Код = "col_a1";
        public const string Назва = "col_a2";
        public const string Запис = "col_a3";
    }

    public class Блокнот_Objest : DirectoryObject
    {
        public Блокнот_Objest() : base(Config.Kernel!, "tab_a01",
             new string[] { "col_a1", "col_a2", "col_a3" }) 
        {
            Код = "";
            Назва = "";
            Запис = "";
            
        }
        
        public void New()
        {
            BaseNew();
            Блокнот_Triggers.New(this);
            
        }

        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Код = base.FieldValue["col_a1"].ToString() ?? "";
                Назва = base.FieldValue["col_a2"].ToString() ?? "";
                Запис = base.FieldValue["col_a3"].ToString() ?? "";
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public bool Save()
        {
            Блокнот_Triggers.BeforeSave(this);
            base.FieldValue["col_a1"] = Код;
            base.FieldValue["col_a2"] = Назва;
            base.FieldValue["col_a3"] = Запис;
            
            bool result = BaseSave();
            if (result)
            {
                Блокнот_Triggers.AfterSave(this);
                BaseWriteFullTextSearch(GetBasis(), new string[] { Запис });
            }
            return result;
        }

        public Блокнот_Objest Copy(bool copyTableParts = false)
        {
            Блокнот_Objest copy = new Блокнот_Objest();
            copy.Код = Код;
            copy.Назва = Назва;
            copy.Запис = Запис;
            
            
            if (copyTableParts)
            {
            
            }

            copy.New();
            Блокнот_Triggers.Copying(copy, this);
            return copy;
        }

        public void SetDeletionLabel(bool label = true)
        {
            Блокнот_Triggers.SetDeletionLabel(this, label);
            base.BaseDeletionLabel(label);
        }

        public void Delete()
        {
            Блокнот_Triggers.BeforeDelete(this);
            base.BaseDelete(new string[] {  });
        }
        
        public Блокнот_Pointer GetDirectoryPointer()
        {
            return new Блокнот_Pointer(UnigueID.UGuid);
        }

        public UuidAndText GetBasis()
        {
            return new UuidAndText(UnigueID.UGuid, "Довідники.Блокнот");
        }

        public string GetPresentation()
        {
            return base.BasePresentation(
                new string[] { "col_a2" }
            );
        }
        
        public string Код { get; set; }
        public string Назва { get; set; }
        public string Запис { get; set; }
        
    }

    public class Блокнот_Pointer : DirectoryPointer
    {
        public Блокнот_Pointer(object? uid = null) : base(Config.Kernel!, "tab_a01")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Блокнот_Pointer(UnigueID uid, Dictionary<string, object>? fields = null) : base(Config.Kernel!, "tab_a01")
        {
            base.Init(uid, fields);
        }
        
        public Блокнот_Objest? GetDirectoryObject()
        {
            if (this.IsEmpty()) return null;
            Блокнот_Objest БлокнотObjestItem = new Блокнот_Objest();
            return БлокнотObjestItem.Read(base.UnigueID) ? БлокнотObjestItem : null;
        }

        public Блокнот_Pointer Copy()
        {
            return new Блокнот_Pointer(base.UnigueID, base.Fields) { Назва = Назва };
        }

        public string Назва { get; set; } = "";

        public string GetPresentation()
        {
            return Назва = base.BasePresentation(
                new string[] { "col_a2" }
            );
        }

        public void SetDeletionLabel(bool label = true)
        {
            Блокнот_Objest? obj = GetDirectoryObject();
            if (obj != null)
            {
                Блокнот_Triggers.SetDeletionLabel(obj, label);
                
                base.BaseDeletionLabel(label);
            }
        }
		
        public Блокнот_Pointer GetEmptyPointer()
        {
            return new Блокнот_Pointer();
        }

        public UuidAndText GetBasis()
        {
            return new UuidAndText(UnigueID.UGuid, "Довідники.Блокнот");
        }

        public void Clear()
        {
            Init(new UnigueID(), null);
            Назва = "";
        }
    }
    
    public class Блокнот_Select : DirectorySelect
    {
        public Блокнот_Select() : base(Config.Kernel!, "tab_a01") { }        
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Блокнот_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Блокнот_Pointer? Current { get; private set; }
        
        public Блокнот_Pointer FindByField(string name, object value)
        {
            Блокнот_Pointer itemPointer = new Блокнот_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(name, value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Блокнот_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Блокнот_Pointer> directoryPointerList = new List<Блокнот_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(name, value, limit, offset)) 
                directoryPointerList.Add(new Блокнот_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
   
    #endregion
    
}

namespace StorageAndTrade_1_0.Перелічення
{
    
    #region ENUM "ТипПеріодуДляЖурналівДокументів"
    public enum ТипПеріодуДляЖурналівДокументів
    {
         ВесьПеріод = 1,
         ЗПочаткуРоку = 2,
         Квартал = 6,
         ЗМинулогоМісяця = 7,
         Місяць = 8,
         ЗПочаткуМісяця = 3,
         ЗПочаткуТижня = 4,
         ПоточнийДень = 5
    }
    #endregion
    

    public static class ПсевдонімиПерелічення
    {
    
        #region ENUM "ТипПеріодуДляЖурналівДокументів"
        public static string ТипПеріодуДляЖурналівДокументів_Alias(ТипПеріодуДляЖурналівДокументів value)
        {
            switch (value)
            {
                
                case ТипПеріодуДляЖурналівДокументів.ВесьПеріод: return "Весь період";
                
                case ТипПеріодуДляЖурналівДокументів.ЗПочаткуРоку: return "Рік (з початку року)";
                
                case ТипПеріодуДляЖурналівДокументів.Квартал: return "Квартал (три місяці)";
                
                case ТипПеріодуДляЖурналівДокументів.ЗМинулогоМісяця: return "Два місяці (з 1 числа)";
                
                case ТипПеріодуДляЖурналівДокументів.Місяць: return "Місяць";
                
                case ТипПеріодуДляЖурналівДокументів.ЗПочаткуМісяця: return "Місяць (з 1 числа)";
                
                case ТипПеріодуДляЖурналівДокументів.ЗПочаткуТижня: return "Тиждень";
                
                case ТипПеріодуДляЖурналівДокументів.ПоточнийДень: return "День";
                
                default: return "";
            }
        }

        public static ТипПеріодуДляЖурналівДокументів? ТипПеріодуДляЖурналівДокументів_FindByName(string name)
        {
            switch (name)
            {
                
                case "Весь період": return ТипПеріодуДляЖурналівДокументів.ВесьПеріод;
                
                case "Рік (з початку року)": return ТипПеріодуДляЖурналівДокументів.ЗПочаткуРоку;
                
                case "Квартал (три місяці)": return ТипПеріодуДляЖурналівДокументів.Квартал;
                
                case "Два місяці (з 1 числа)": return ТипПеріодуДляЖурналівДокументів.ЗМинулогоМісяця;
                
                case "Місяць": return ТипПеріодуДляЖурналівДокументів.Місяць;
                
                case "Місяць (з 1 числа)": return ТипПеріодуДляЖурналівДокументів.ЗПочаткуМісяця;
                
                case "Тиждень": return ТипПеріодуДляЖурналівДокументів.ЗПочаткуТижня;
                
                case "День": return ТипПеріодуДляЖурналівДокументів.ПоточнийДень;
                
                default: return null;
            }
        }

        public static List<NameValue<ТипПеріодуДляЖурналівДокументів>> ТипПеріодуДляЖурналівДокументів_List()
        {
            List<NameValue<ТипПеріодуДляЖурналівДокументів>> value = new List<NameValue<ТипПеріодуДляЖурналівДокументів>>();
            
            value.Add(new NameValue<ТипПеріодуДляЖурналівДокументів>("Весь період", ТипПеріодуДляЖурналівДокументів.ВесьПеріод));
            
            value.Add(new NameValue<ТипПеріодуДляЖурналівДокументів>("Рік (з початку року)", ТипПеріодуДляЖурналівДокументів.ЗПочаткуРоку));
            
            value.Add(new NameValue<ТипПеріодуДляЖурналівДокументів>("Квартал (три місяці)", ТипПеріодуДляЖурналівДокументів.Квартал));
            
            value.Add(new NameValue<ТипПеріодуДляЖурналівДокументів>("Два місяці (з 1 числа)", ТипПеріодуДляЖурналівДокументів.ЗМинулогоМісяця));
            
            value.Add(new NameValue<ТипПеріодуДляЖурналівДокументів>("Місяць", ТипПеріодуДляЖурналівДокументів.Місяць));
            
            value.Add(new NameValue<ТипПеріодуДляЖурналівДокументів>("Місяць (з 1 числа)", ТипПеріодуДляЖурналівДокументів.ЗПочаткуМісяця));
            
            value.Add(new NameValue<ТипПеріодуДляЖурналівДокументів>("Тиждень", ТипПеріодуДляЖурналівДокументів.ЗПочаткуТижня));
            
            value.Add(new NameValue<ТипПеріодуДляЖурналівДокументів>("День", ТипПеріодуДляЖурналівДокументів.ПоточнийДень));
            
            return value;
        }
        #endregion
    
    }
}

namespace StorageAndTrade_1_0.Документи
{
    
    #region DOCUMENT "Подія"
    public static class Подія_Const
    {
        public const string FULLNAME = "Подія";
        public const string TABLE = "tab_a03";
        public const string DELETION_LABEL = "deletion_label";
        
        
        public const string Назва = "docname";
        public const string ДатаДок = "docdate";
        public const string НомерДок = "docnomer";
        public const string Коментар = "col_a1";
    }

    public static class Подія_Export
    {
        public static void ToXmlFile(Подія_Pointer Подія, string pathToSave)
        {
            Подія_Objest? obj = Подія.GetDocumentObject(true);
            if (obj == null) return;

            XmlWriter xmlWriter = XmlWriter.Create(pathToSave, new XmlWriterSettings() { Indent = true, Encoding = System.Text.Encoding.UTF8 });
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("root");
            xmlWriter.WriteAttributeString("uid", obj.UnigueID.ToString());
            
            xmlWriter.WriteStartElement("Назва");
            xmlWriter.WriteAttributeString("type", "string");
            
                xmlWriter.WriteValue(obj.Назва);
              
            xmlWriter.WriteEndElement(); //Назва
            xmlWriter.WriteStartElement("ДатаДок");
            xmlWriter.WriteAttributeString("type", "datetime");
            
                xmlWriter.WriteValue(obj.ДатаДок);
              
            xmlWriter.WriteEndElement(); //ДатаДок
            xmlWriter.WriteStartElement("НомерДок");
            xmlWriter.WriteAttributeString("type", "string");
            
                xmlWriter.WriteValue(obj.НомерДок);
              
            xmlWriter.WriteEndElement(); //НомерДок
            xmlWriter.WriteStartElement("Коментар");
            xmlWriter.WriteAttributeString("type", "string");
            
                xmlWriter.WriteValue(obj.Коментар);
              
            xmlWriter.WriteEndElement(); //Коментар

            xmlWriter.WriteEndElement(); //root
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }
    }

    public class Подія_Objest : DocumentObject
    {
        public Подія_Objest() : base(Config.Kernel!, "tab_a03", "Подія",
             new string[] { "docname", "docdate", "docnomer", "col_a1" }) 
        {
            Назва = "";
            ДатаДок = DateTime.MinValue;
            НомерДок = "";
            Коментар = "";
            
        }
        
        public void New()
        {
            BaseNew();
            Подія_Triggers.New(this);
            
        }

        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["docname"].ToString() ?? "";
                ДатаДок = (base.FieldValue["docdate"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["docdate"].ToString() ?? DateTime.MinValue.ToString()) : DateTime.MinValue;
                НомерДок = base.FieldValue["docnomer"].ToString() ?? "";
                Коментар = base.FieldValue["col_a1"].ToString() ?? "";
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public bool Save()
        {
            Подія_Triggers.BeforeSave(this);
            base.FieldValue["docname"] = Назва;
            base.FieldValue["docdate"] = ДатаДок;
            base.FieldValue["docnomer"] = НомерДок;
            base.FieldValue["col_a1"] = Коментар;
            
            bool result = BaseSave();
            
            if (result)
            {
                Подія_Triggers.AfterSave(this);
                BaseWriteFullTextSearch(GetBasis(), new string[] {  });
            }

            return result;
        }

        public bool SpendTheDocument(DateTime spendDate)
        {
            bool rezult = Подія_SpendTheDocument.Spend(this);
                BaseSpend(rezult, spendDate);
                return rezult;
        }

        public void ClearSpendTheDocument()
        {
            Подія_SpendTheDocument.ClearSpend(this);
            BaseSpend(false, DateTime.MinValue);
        }

        public Подія_Objest Copy(bool copyTableParts = false)
        {
            Подія_Objest copy = new Подія_Objest();
            copy.Назва = Назва;
            copy.ДатаДок = ДатаДок;
            copy.НомерДок = НомерДок;
            copy.Коментар = Коментар;
            

            if (copyTableParts)
            {
            
            }

            copy.New();
            Подія_Triggers.Copying(copy, this);
            return copy;
        }

        public void SetDeletionLabel(bool label = true)
        {
            Подія_Triggers.SetDeletionLabel(this, label);
            ClearSpendTheDocument();
            base.BaseDeletionLabel(label);
        }

        public void Delete()
        {
            Подія_Triggers.BeforeDelete(this);
            ClearSpendTheDocument();
            base.BaseDelete(new string[] {  });
        }
        
        public Подія_Pointer GetDocumentPointer()
        {
            return new Подія_Pointer(UnigueID.UGuid);
        }

        public UuidAndText GetBasis()
        {
            return new UuidAndText(UnigueID.UGuid, "Документи.Подія");
        }
        
        public string Назва { get; set; }
        public DateTime ДатаДок { get; set; }
        public string НомерДок { get; set; }
        public string Коментар { get; set; }
        
    }
    
    public class Подія_Pointer : DocumentPointer
    {
        public Подія_Pointer(object? uid = null) : base(Config.Kernel!, "tab_a03", "Подія")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Подія_Pointer(UnigueID uid, Dictionary<string, object>? fields = null) : base(Config.Kernel!, "tab_a03", "Подія")
        {
            base.Init(uid, fields);
        }

        public string Назва { get; set; } = "";

        public string GetPresentation()
        {
            return Назва = base.BasePresentation(
              new string[] { "docname" }
            );
        }

        public bool SpendTheDocument(DateTime spendDate)
        {
            Подія_Objest? obj = GetDocumentObject();
            return (obj != null ? obj.SpendTheDocument(spendDate) : false);
        }

        public void ClearSpendTheDocument()
        {
            Подія_Objest? obj = GetDocumentObject();
            if (obj != null) obj.ClearSpendTheDocument();
        }

        public void SetDeletionLabel(bool label = true)
        {
            Подія_Objest? obj = GetDocumentObject();
                if (obj == null) return;
                Подія_Triggers.SetDeletionLabel(obj, label);
                
                if (label)
                {
                    Подія_SpendTheDocument.ClearSpend(obj);
                    BaseSpend(false, DateTime.MinValue);
                }
                
            base.BaseDeletionLabel(label);
        }

        public Подія_Pointer Copy()
        {
            return new Подія_Pointer(base.UnigueID, base.Fields) { Назва = Назва };
        }

        public Подія_Pointer GetEmptyPointer()
        {
            return new Подія_Pointer();
        }

        public UuidAndText GetBasis()
        {
            return new UuidAndText(UnigueID.UGuid, "Документи.Подія");
        }

        public Подія_Objest? GetDocumentObject(bool readAllTablePart = false)
        {
            if (this.IsEmpty()) return null;
            Подія_Objest ПодіяObjestItem = new Подія_Objest();
            if (!ПодіяObjestItem.Read(base.UnigueID)) return null;
            
            return ПодіяObjestItem;
        }

        public void Clear()
        {
            Init(new UnigueID(), null);
            Назва = "";
        }
    }

    public class Подія_Select : DocumentSelect
    {		
        public Подія_Select() : base(Config.Kernel!, "tab_a03") { }
        
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Подія_Pointer(base.DocumentPointerPosition.UnigueID, base.DocumentPointerPosition.Fields); return true; } else { Current = null; return false; } }
        
        public Подія_Pointer? Current { get; private set; }
    }

      
    
    #endregion
    
}

namespace StorageAndTrade_1_0.Журнали
{
    #region Journal
    public class Journal_Select: JournalSelect
    {
        public Journal_Select() : base(Config.Kernel!,
             new string[] { "tab_a03"},
			       new string[] { "Подія"}) { }

        public DocumentObject? GetDocumentObject(bool readAllTablePart = true)
        {
            if (Current == null)
                return null;

            
			
			      return null;
        }
    }
    #endregion

}

namespace StorageAndTrade_1_0.РегістриВідомостей
{
    
}

namespace StorageAndTrade_1_0.РегістриНакопичення
{
    public static class VirtualTablesСalculation
    {
        /* Функція повного очищення віртуальних таблиць */
        public static void ClearAll()
        {
            /*  */
        }

        /* Функція для обчислення віртуальних таблиць  */
        public static void Execute(DateTime period, string regAccumName)
        {
            
        }

        /* Функція для обчислення підсумкових віртуальних таблиць */
        public static void ExecuteFinalCalculation(List<string> regAccumNameList)
        {
            
        }
    }

    
}
  