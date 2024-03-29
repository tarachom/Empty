﻿/*
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
 
Модуль проведення документів
 
*/

using AccountingSoftware;
using StorageAndTrade;

using StorageAndTrade_1_0.Довідники;
using StorageAndTrade_1_0.РегістриНакопичення;
using StorageAndTrade_1_0.РегістриВідомостей;

namespace StorageAndTrade_1_0.Документи
{
    class СпільніФункції
    {
        /// <summary>
		/// Перервати проведення документу
		/// </summary>
		/// <param name="ДокументОбєкт">Документ обєкт</param>
		/// <param name="НазваДокументу">Назва документу</param>
		/// <param name="СписокПомилок">Список помилок</param>
        public static async void ДокументНеПроводиться(DocumentObject ДокументОбєкт, string НазваДокументу, string СписокПомилок)
        {
            await ФункціїДляПовідомлень.ДодатиПовідомленняПроПомилку(
                DateTime.Now, "Проведення документу", ДокументОбєкт.UnigueID.UGuid, $"Документ.{ДокументОбєкт.TypeDocument}", НазваДокументу,
                СписокПомилок + "\n\nДокумент [" + НазваДокументу + "] не проводиться!");
        }
    }

    


}
