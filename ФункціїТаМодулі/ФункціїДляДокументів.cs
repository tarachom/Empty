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

Функції для документів

Функції для шапки
Контекстне меню для табличної частини

*/

using AccountingSoftware;
using Конфа = StorageAndTrade_1_0;
using Довідники = StorageAndTrade_1_0.Довідники;
using Документи = StorageAndTrade_1_0.Документи;
using Перелічення = StorageAndTrade_1_0.Перелічення;
using РегістриВідомостей = StorageAndTrade_1_0.РегістриВідомостей;

namespace StorageAndTrade
{
    /// <summary>
    /// Спільні функції для документів
    /// </summary>
    class ФункціїДляДокументів
    {
        /// <summary>
        /// Функція обєднує дві дати (з пешої дата, з другої час)
        /// </summary>
        /// <param name="дата">Дата</param>
        /// <param name="час">Час</param>
        /// <returns>Обєднана дата</returns>
        public static DateTime ОбєднатиДатуТаЧас(DateTime дата, DateTime час)
        {
            return new DateTime(дата.Year, дата.Month, дата.Day, час.Hour, час.Minute, час.Second);
        }
    }
}
