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

Довідники

*/


using Gtk;

using AccountingSoftware;

using StorageAndTrade_1_0;
using StorageAndTrade_1_0.Довідники;

namespace StorageAndTrade
{
    class PageDirectory : VBox
    {
        public PageDirectory() : base()
        {
            //Всі Довідники
            {
                HBox hBoxAll = new HBox(false, 0);
                PackStart(hBoxAll, false, false, 10);

                Expander expanderAll = new Expander("Всі довідники");
                hBoxAll.PackStart(expanderAll, false, false, 5);

                VBox vBoxAll = new VBox(false, 0);
                expanderAll.Add(vBoxAll);

                vBoxAll.PackStart(new Label("Довідники"), false, false, 2);

                ListBox listBox = new ListBox();
                listBox.ButtonPressEvent += (object? sender, ButtonPressEventArgs args) =>
                {
                    if (args.Event.Type == Gdk.EventType.DoubleButtonPress && listBox.SelectedRows.Length != 0)
                        ФункціїДляДовідників.ВідкритиДовідникВідповідноДоВиду(listBox.SelectedRows[0].Name, null, false);
                };

                ScrolledWindow scrollList = new ScrolledWindow() { WidthRequest = 300, HeightRequest = 300, ShadowType = ShadowType.In };
                scrollList.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);
                scrollList.Add(listBox);

                vBoxAll.PackStart(scrollList, false, false, 2);

                foreach (KeyValuePair<string, ConfigurationDirectories> directories in Config.Kernel!.Conf.Directories)
                {
                    string title = String.IsNullOrEmpty(directories.Value.FullName) ? directories.Value.Name : directories.Value.FullName;

                    ListBoxRow row = new ListBoxRow() { Name = directories.Key };
                    row.Add(new Label(title) { Halign = Align.Start });

                    listBox.Add(row);
                }
            }

            //Список
            HBox hBoxList = new HBox(false, 0);
            PackStart(hBoxList, false, false, 10);

            VBox vLeft = new VBox(false, 0);
            hBoxList.PackStart(vLeft, false, false, 5);

            Link.AddLink(vLeft, $"{Блокнот_Const.FULLNAME}", async () =>
            {
                Блокнот page = new Блокнот();
                Program.GeneralForm?.CreateNotebookPage($"{Блокнот_Const.FULLNAME}", () => { return page; });
                await page.LoadRecords();
            });

            ShowAll();
        }
    }
}