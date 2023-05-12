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

Журнали

*/

using Gtk;

namespace StorageAndTrade
{
    class PageJournals : VBox
    {
        public PageJournals() : base()
        {
            //Список
            HBox hBoxList = new HBox(false, 0);
            PackStart(hBoxList, false, false, 10);

            VBox vLeft = new VBox(false, 0);
            hBoxList.PackStart(vLeft, false, false, 5);

            Link.AddLink(vLeft, "Повний", () =>
            {
                Журнал_Повний page = new Журнал_Повний();
                Program.GeneralForm?.CreateNotebookPage("Повний", () => { return page; });
                page.SetValue();
            });

            ShowAll();
        }

        void AddLink(VBox vbox, string uri, EventHandler? clickAction = null)
        {
            LinkButton lb = new LinkButton(uri, " " + uri) { Halign = Align.Start, Image = new Image(AppContext.BaseDirectory + "images/doc.png"), AlwaysShowImage = true };
            vbox.PackStart(lb, false, false, 0);

            if (clickAction != null)
                lb.Clicked += clickAction;
        }
    }
}