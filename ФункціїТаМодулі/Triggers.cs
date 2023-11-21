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
 
Модуль функцій зворотнього виклику.

1. Перед записом
2. Після запису
3. Перед видаленням
 
*/

using AccountingSoftware;
using StorageAndTrade;
using Конфа = StorageAndTrade_1_0;
using StorageAndTrade_1_0.Константи;
using StorageAndTrade_1_0.Довідники;

namespace StorageAndTrade_1_0.Довідники
{
    class Користувачі_Triggers
    {
        public static void New(Користувачі_Objest ДовідникОбєкт)
        {
            ДовідникОбєкт.Код = (++НумераціяДовідників.Користувачі_Const).ToString("D6");
        }

        public static async ValueTask Copying(Користувачі_Objest ДовідникОбєкт, Користувачі_Objest Основа)
        {
            ДовідникОбєкт.Назва += " - Копія";

            await ValueTask.FromResult(true);
        }

        public static void BeforeSave(Користувачі_Objest ДовідникОбєкт)
        {

        }

        public static void AfterSave(Користувачі_Objest ДовідникОбєкт)
        {

        }

        public static async ValueTask SetDeletionLabel(Користувачі_Objest ДовідникОбєкт, bool label)
        {
            await ValueTask.FromResult(true);
        }

        public static async ValueTask BeforeDelete(Користувачі_Objest ДовідникОбєкт)
        {
            await ValueTask.FromResult(true);
        }
    }

    class Блокнот_Triggers
    {
        public static void New(Блокнот_Objest ДовідникОбєкт)
        {
            ДовідникОбєкт.Код = (++НумераціяДовідників.Блокнот_Const).ToString("D6");
        }

        public static async ValueTask Copying(Блокнот_Objest ДовідникОбєкт, Блокнот_Objest Основа)
        {
            ДовідникОбєкт.Назва += " - Копія";

            await ValueTask.FromResult(true);
        }

        public static void BeforeSave(Блокнот_Objest ДовідникОбєкт)
        {

        }

        public static void AfterSave(Блокнот_Objest ДовідникОбєкт)
        {

        }

        public static async ValueTask SetDeletionLabel(Блокнот_Objest ДовідникОбєкт, bool label)
        {
            await ValueTask.FromResult(true);
        }

        public static async ValueTask BeforeDelete(Блокнот_Objest ДовідникОбєкт)
        {
            await ValueTask.FromResult(true);
        }
    }

}

namespace StorageAndTrade_1_0.Документи
{



}