
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
 * Цей код згенерований в Конфігураторі 3. Шаблон CodeGeneration.xslt
 *
 */

using AccountingSoftware;
using System.Xml;

namespace StorageAndTrade_1_0
{
    public static class Config
    {
        public static Kernel Kernel { get; set; } = new Kernel();
		
        public static async ValueTask ReadAllConstants()
        {
            await Константи.Системні.ReadAll();
            await Константи.ЖурналиДокументів.ReadAll();
            await Константи.ПриЗапускуПрограми.ReadAll();
            await Константи.НумераціяДовідників.ReadAll();
            await Константи.НумераціяДокументів.ReadAll();
            
        }
    }

    public class Functions
    {
        public record CompositePointerPresentation_Record
        {
            public string result = "";
            public string pointer = "";
            public string type = "";
        }
        /*
          Функція для типу який задається користувачем.
          Повертає презентацію для uuidAndText.
          В @pointer - повертає групу (Документи або Довідники)
            @type - повертає назву типу
        */
        public static async ValueTask<CompositePointerPresentation_Record> CompositePointerPresentation(UuidAndText uuidAndText)
        {
            CompositePointerPresentation_Record record = new();

            if (uuidAndText.IsEmpty() || string.IsNullOrEmpty(uuidAndText.Text) || uuidAndText.Text.IndexOf(".") == -1)
                return record;

            string[] pointer_and_type = uuidAndText.Text.Split(".", StringSplitOptions.None);

            if (pointer_and_type.Length == 2)
            {
                record.pointer = pointer_and_type[0];
                record.type = pointer_and_type[1];

                if (record.pointer == "Документи")
                {
                    
                    return record;
                    
                }
                else if (record.pointer == "Довідники")
                {
                    
                    switch (record.type)
                    {
                        
                        case "Користувачі": record.result = await new Довідники.Користувачі_Pointer(uuidAndText.Uuid).GetPresentation(); return record;
                        
                        case "Блокнот": record.result = await new Довідники.Блокнот_Pointer(uuidAndText.Uuid).GetPresentation(); return record;
                        
                    }
                    
                }
            }

            return record;
        }
    }
}

namespace StorageAndTrade_1_0.Константи
{
    
	  #region CONSTANTS BLOCK "Системні"
    public static class Системні
    {
        public static async ValueTask ReadAll()
        {
            
            Dictionary<string, object> fieldValue = [];
            bool IsSelect = await Config.Kernel.DataBase.SelectAllConstants("tab_constants",
                 ["col_a2", "col_a9", ], fieldValue);
            
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
                Config.Kernel.DataBase.SaveConstants("tab_constants", "col_a2", m_ЗупинитиФоновіЗадачі_Const);
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
                Config.Kernel.DataBase.SaveConstants("tab_constants", "col_a9", m_ПовідомленняТаПомилки_Const);
            }
        }
        
        
        public class ПовідомленняТаПомилки_Помилки_TablePart : ConstantsTablePart
        {
            public ПовідомленняТаПомилки_Помилки_TablePart() : base(Config.Kernel, "tab_a02",
                 ["col_a1", "col_a2", "col_a3", "col_a4", "col_a5", "col_a6", ]) { }
            
            public const string TABLE = "tab_a02";
            
            public const string Дата = "col_a1";
            public const string НазваПроцесу = "col_a2";
            public const string Обєкт = "col_a3";
            public const string ТипОбєкту = "col_a4";
            public const string НазваОбєкту = "col_a5";
            public const string Повідомлення = "col_a6";
            public List<Record> Records { get; set; } = [];
        
            public async ValueTask Read()
            {
                Records.Clear();
                await base.BaseRead();

                foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
                {
                    Record record = new Record()
                    {
                        UID = (Guid)fieldValue["uid"],
                        Дата = (fieldValue["col_a1"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a1"].ToString() ?? DateTime.MinValue.ToString()) : DateTime.MinValue,
                        НазваПроцесу = fieldValue["col_a2"].ToString() ?? "",
                        Обєкт = (fieldValue["col_a3"] != DBNull.Value) ? (Guid)fieldValue["col_a3"] : Guid.Empty,
                        ТипОбєкту = fieldValue["col_a4"].ToString() ?? "",
                        НазваОбєкту = fieldValue["col_a5"].ToString() ?? "",
                        Повідомлення = fieldValue["col_a6"].ToString() ?? "",
                        
                    };
                    Records.Add(record);
                }
            
                base.BaseClear();
            }
        
            public async ValueTask Save(bool clear_all_before_save /*= true*/) 
            {
                await base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    await base.BaseDelete();

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>()
                    {
                        {"col_a1", record.Дата},
                        {"col_a2", record.НазваПроцесу},
                        {"col_a3", record.Обєкт},
                        {"col_a4", record.ТипОбєкту},
                        {"col_a5", record.НазваОбєкту},
                        {"col_a6", record.Повідомлення},
                        
                    };
                    record.UID = await base.BaseSave(record.UID, fieldValue);
                }
                
                await base.BaseCommitTransaction();
            }
        
            public async ValueTask Delete()
            {
                await base.BaseDelete();
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
        public static async ValueTask ReadAll()
        {
            
            Dictionary<string, object> fieldValue = [];
            bool IsSelect = await Config.Kernel.DataBase.SelectAllConstants("tab_constants",
                 ["col_a8", ], fieldValue);
            
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
                Config.Kernel.DataBase.SaveConstants("tab_constants", "col_a8", (int)m_ОсновнийТипПеріоду_Const);
            }
        }
             
    }
    #endregion
    
	  #region CONSTANTS BLOCK "ПриЗапускуПрограми"
    public static class ПриЗапускуПрограми
    {
        public static async ValueTask ReadAll()
        {
            
        }
        
             
    }
    #endregion
    
	  #region CONSTANTS BLOCK "НумераціяДовідників"
    public static class НумераціяДовідників
    {
        public static async ValueTask ReadAll()
        {
            
            Dictionary<string, object> fieldValue = [];
            bool IsSelect = await Config.Kernel.DataBase.SelectAllConstants("tab_constants",
                 ["col_a1", "col_a3", ], fieldValue);
            
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
                Config.Kernel.DataBase.SaveConstants("tab_constants", "col_a1", m_Користувачі_Const);
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
                Config.Kernel.DataBase.SaveConstants("tab_constants", "col_a3", m_Блокнот_Const);
            }
        }
             
    }
    #endregion
    
	  #region CONSTANTS BLOCK "НумераціяДокументів"
    public static class НумераціяДокументів
    {
        public static async ValueTask ReadAll()
        {
            
        }
        
             
    }
    #endregion
    
}

namespace StorageAndTrade_1_0.Довідники
{
    
    #region DIRECTORY "Користувачі"
    public static class Користувачі_Const
    {
        public const string TABLE = "tab_a08";
        public const string POINTER = "Довідники.Користувачі";
        public const string FULLNAME = "Користувачі";
        public const string DELETION_LABEL = "deletion_label";
        
        public const string Код = "col_a1";
        public const string Назва = "col_a2";
        public const string КодВСпеціальнійТаблиці = "col_a3";
        public const string Коментар = "col_a4";
        public const string Заблокований = "col_a5";
    }

    public class Користувачі_Objest : DirectoryObject
    {
        public Користувачі_Objest() : base(Config.Kernel, "tab_a08",
             ["col_a1", "col_a2", "col_a3", "col_a4", "col_a5", ]) 
        {
            Код = "";
            Назва = "";
            КодВСпеціальнійТаблиці = new Guid();
            Коментар = "";
            Заблокований = false;
            
        }
        
        public async ValueTask New()
        {
            BaseNew();
            
                await Користувачі_Triggers.New(this);
              
        }

        public async ValueTask<bool> Read(UnigueID uid)
        {
            if (await BaseRead(uid))
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

        /* синхронна функція для Read(UnigueID uid) */
        public bool ReadSync(UnigueID uid) { return Task.Run<bool>(async () => { return await Read(uid); }).Result; }
        
        public async ValueTask<bool> Save()
        {
            
                await Користувачі_Triggers.BeforeSave(this);
            base.FieldValue["col_a1"] = Код;
            base.FieldValue["col_a2"] = Назва;
            base.FieldValue["col_a3"] = КодВСпеціальнійТаблиці;
            base.FieldValue["col_a4"] = Коментар;
            base.FieldValue["col_a5"] = Заблокований;
            
            bool result = await BaseSave();
            if (result)
            {
                
                    await Користувачі_Triggers.AfterSave(this);
                await BaseWriteFullTextSearch(GetBasis(), [Назва, Коментар, ]);
            }
            return result;
        }

        public async ValueTask<Користувачі_Objest> Copy(bool copyTableParts = false)
        {
            Користувачі_Objest copy = new Користувачі_Objest()
            {
                Код = Код,
                Назва = Назва,
                КодВСпеціальнійТаблиці = КодВСпеціальнійТаблиці,
                Коментар = Коментар,
                Заблокований = Заблокований,
                
            };
            

            await copy.New();
            
                await Користувачі_Triggers.Copying(copy, this);
            return copy;
                
        }

        public async ValueTask SetDeletionLabel(bool label = true)
        {
            
                await Користувачі_Triggers.SetDeletionLabel(this, label);
            await base.BaseDeletionLabel(label);
        }

        public async ValueTask Delete()
        {
            
                await Користувачі_Triggers.BeforeDelete(this);
            await base.BaseDelete(new string[] {  });
        }

        /* синхронна функція для Delete() */
        public bool DeleteSync() { return Task.Run<bool>(async () => { await Delete(); return true; }).Result; } 
        
        public Користувачі_Pointer GetDirectoryPointer()
        {
            return new Користувачі_Pointer(UnigueID.UGuid);
        }

        public UuidAndText GetBasis()
        {
            return new UuidAndText(UnigueID.UGuid, Користувачі_Const.POINTER);
        }

        public async ValueTask<string> GetPresentation()
        {
            return await base.BasePresentation(
                ["col_a2", ]
            );
        }
        
        /* синхронна функція для GetPresentation() */
        public string GetPresentationSync() { return Task.Run<string>(async () => { return await GetPresentation(); }).Result; }
        
        public string Код { get; set; }
        public string Назва { get; set; }
        public Guid КодВСпеціальнійТаблиці { get; set; }
        public string Коментар { get; set; }
        public bool Заблокований { get; set; }
        
    }

    public class Користувачі_Pointer : DirectoryPointer
    {
        public Користувачі_Pointer(object? uid = null) : base(Config.Kernel, "tab_a08")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Користувачі_Pointer(UnigueID uid, Dictionary<string, object>? fields = null) : base(Config.Kernel, "tab_a08")
        {
            base.Init(uid, fields);
        }
        
        public async ValueTask<Користувачі_Objest?> GetDirectoryObject()
        {
            if (this.IsEmpty()) return null;
            Користувачі_Objest КористувачіObjestItem = new Користувачі_Objest();
            return await КористувачіObjestItem.Read(base.UnigueID) ? КористувачіObjestItem : null;
        }

        public Користувачі_Pointer Copy()
        {
            return new Користувачі_Pointer(base.UnigueID, base.Fields) { Назва = Назва };
        }

        public string Назва { get; set; } = "";

        public async ValueTask<string> GetPresentation()
        {
            return Назва = await base.BasePresentation(
                ["col_a2", ]
            );
        }

        /* синхронна функція для GetPresentation() */
        public string GetPresentationSync() { return Task.Run<string>(async () => { return await GetPresentation(); }).Result; }

        public async ValueTask SetDeletionLabel(bool label = true)
        {
            Користувачі_Objest? obj = await GetDirectoryObject();
            if (obj != null)
            {
                
                    await Користувачі_Triggers.SetDeletionLabel(obj, label);
                
                await base.BaseDeletionLabel(label);
            }
        }
		
        public Користувачі_Pointer GetEmptyPointer()
        {
            return new Користувачі_Pointer();
        }

        public UuidAndText GetBasis()
        {
            return new UuidAndText(UnigueID.UGuid, Користувачі_Const.POINTER);
        }

        public void Clear()
        {
            Init(new UnigueID(), null);
            Назва = "";
        }
    }
    
    public class Користувачі_Select : DirectorySelect
    {
        public Користувачі_Select() : base(Config.Kernel, "tab_a08") { }        
        public async ValueTask<bool> Select() { return await base.BaseSelect(); }
        
        public async ValueTask<bool> SelectSingle() { if (await base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Користувачі_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Користувачі_Pointer? Current { get; private set; }
        
        public async ValueTask<Користувачі_Pointer> FindByField(string name, object value)
        {
            Користувачі_Pointer itemPointer = new Користувачі_Pointer();
            DirectoryPointer directoryPointer = await base.BaseFindByField(name, value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public async ValueTask<List<Користувачі_Pointer>> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Користувачі_Pointer> directoryPointerList = new List<Користувачі_Pointer>();
            foreach (DirectoryPointer directoryPointer in await base.BaseFindListByField(name, value, limit, offset)) 
                directoryPointerList.Add(new Користувачі_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
   
    #endregion
    
    #region DIRECTORY "Блокнот"
    public static class Блокнот_Const
    {
        public const string TABLE = "tab_a01";
        public const string POINTER = "Довідники.Блокнот";
        public const string FULLNAME = "Блокнот";
        public const string DELETION_LABEL = "deletion_label";
        
        public const string Код = "col_a1";
        public const string Назва = "col_a2";
        public const string Запис = "col_a3";
    }

    public class Блокнот_Objest : DirectoryObject
    {
        public Блокнот_Objest() : base(Config.Kernel, "tab_a01",
             ["col_a1", "col_a2", "col_a3", ]) 
        {
            Код = "";
            Назва = "";
            Запис = "";
            
        }
        
        public async ValueTask New()
        {
            BaseNew();
            
                await Блокнот_Triggers.New(this);
              
        }

        public async ValueTask<bool> Read(UnigueID uid)
        {
            if (await BaseRead(uid))
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

        /* синхронна функція для Read(UnigueID uid) */
        public bool ReadSync(UnigueID uid) { return Task.Run<bool>(async () => { return await Read(uid); }).Result; }
        
        public async ValueTask<bool> Save()
        {
            
                await Блокнот_Triggers.BeforeSave(this);
            base.FieldValue["col_a1"] = Код;
            base.FieldValue["col_a2"] = Назва;
            base.FieldValue["col_a3"] = Запис;
            
            bool result = await BaseSave();
            if (result)
            {
                
                    await Блокнот_Triggers.AfterSave(this);
                await BaseWriteFullTextSearch(GetBasis(), [Назва, Запис, ]);
            }
            return result;
        }

        public async ValueTask<Блокнот_Objest> Copy(bool copyTableParts = false)
        {
            Блокнот_Objest copy = new Блокнот_Objest()
            {
                Код = Код,
                Назва = Назва,
                Запис = Запис,
                
            };
            

            await copy.New();
            
                await Блокнот_Triggers.Copying(copy, this);
            return copy;
                
        }

        public async ValueTask SetDeletionLabel(bool label = true)
        {
            
                await Блокнот_Triggers.SetDeletionLabel(this, label);
            await base.BaseDeletionLabel(label);
        }

        public async ValueTask Delete()
        {
            
                await Блокнот_Triggers.BeforeDelete(this);
            await base.BaseDelete(new string[] {  });
        }

        /* синхронна функція для Delete() */
        public bool DeleteSync() { return Task.Run<bool>(async () => { await Delete(); return true; }).Result; } 
        
        public Блокнот_Pointer GetDirectoryPointer()
        {
            return new Блокнот_Pointer(UnigueID.UGuid);
        }

        public UuidAndText GetBasis()
        {
            return new UuidAndText(UnigueID.UGuid, Блокнот_Const.POINTER);
        }

        public async ValueTask<string> GetPresentation()
        {
            return await base.BasePresentation(
                ["col_a2", ]
            );
        }
        
        /* синхронна функція для GetPresentation() */
        public string GetPresentationSync() { return Task.Run<string>(async () => { return await GetPresentation(); }).Result; }
        
        public string Код { get; set; }
        public string Назва { get; set; }
        public string Запис { get; set; }
        
    }

    public class Блокнот_Pointer : DirectoryPointer
    {
        public Блокнот_Pointer(object? uid = null) : base(Config.Kernel, "tab_a01")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Блокнот_Pointer(UnigueID uid, Dictionary<string, object>? fields = null) : base(Config.Kernel, "tab_a01")
        {
            base.Init(uid, fields);
        }
        
        public async ValueTask<Блокнот_Objest?> GetDirectoryObject()
        {
            if (this.IsEmpty()) return null;
            Блокнот_Objest БлокнотObjestItem = new Блокнот_Objest();
            return await БлокнотObjestItem.Read(base.UnigueID) ? БлокнотObjestItem : null;
        }

        public Блокнот_Pointer Copy()
        {
            return new Блокнот_Pointer(base.UnigueID, base.Fields) { Назва = Назва };
        }

        public string Назва { get; set; } = "";

        public async ValueTask<string> GetPresentation()
        {
            return Назва = await base.BasePresentation(
                ["col_a2", ]
            );
        }

        /* синхронна функція для GetPresentation() */
        public string GetPresentationSync() { return Task.Run<string>(async () => { return await GetPresentation(); }).Result; }

        public async ValueTask SetDeletionLabel(bool label = true)
        {
            Блокнот_Objest? obj = await GetDirectoryObject();
            if (obj != null)
            {
                
                    await Блокнот_Triggers.SetDeletionLabel(obj, label);
                
                await base.BaseDeletionLabel(label);
            }
        }
		
        public Блокнот_Pointer GetEmptyPointer()
        {
            return new Блокнот_Pointer();
        }

        public UuidAndText GetBasis()
        {
            return new UuidAndText(UnigueID.UGuid, Блокнот_Const.POINTER);
        }

        public void Clear()
        {
            Init(new UnigueID(), null);
            Назва = "";
        }
    }
    
    public class Блокнот_Select : DirectorySelect
    {
        public Блокнот_Select() : base(Config.Kernel, "tab_a01") { }        
        public async ValueTask<bool> Select() { return await base.BaseSelect(); }
        
        public async ValueTask<bool> SelectSingle() { if (await base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Блокнот_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Блокнот_Pointer? Current { get; private set; }
        
        public async ValueTask<Блокнот_Pointer> FindByField(string name, object value)
        {
            Блокнот_Pointer itemPointer = new Блокнот_Pointer();
            DirectoryPointer directoryPointer = await base.BaseFindByField(name, value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public async ValueTask<List<Блокнот_Pointer>> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Блокнот_Pointer> directoryPointerList = new List<Блокнот_Pointer>();
            foreach (DirectoryPointer directoryPointer in await base.BaseFindListByField(name, value, limit, offset)) 
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
    
}

namespace StorageAndTrade_1_0.Журнали
{
    #region Journal
    public class Journal_Select: JournalSelect
    {
        public Journal_Select() : base(Config.Kernel,
             [],
			       []) { }

        public async ValueTask<DocumentObject?> GetDocumentObject(bool readAllTablePart = true)
        {
            if (Current == null) return null;
            switch (Current.TypeDocument)
            {
                
                default: return null;
            }
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
        public static async ValueTask Execute(DateTime period, string regAccumName)
        {
            if (Config.Kernel == null) return;
            
        }

        /* Функція для обчислення підсумкових віртуальних таблиць */
        public static async ValueTask ExecuteFinalCalculation(List<string> regAccumNameList)
        {
            if (Config.Kernel == null) return;
            
        }
    }

    
}
  