try
            {
                DBConnection();

                dtMenuSetup = new DataTable();
                string sql = "select * from [RUTEC_BASIC].[dbo].[MENU_MST]";
                using (cmd = new SqlCommand(sql, conn))
                using (rd = cmd.ExecuteReader())
                {
                    dtMenuSetup.Load(rd);
                }

                Dictionary<string, AccordionControlElement> parentElements = new Dictionary<string, AccordionControlElement>();

                foreach (DataRow row in dtMenuSetup.Rows)
                {
                    string menuCode = row["MENU_CODE"].ToString();
                    string menuName = row["MENU_NAME"].ToString();
                    string parentCode = row["PARENT_CODE"].ToString();

                    var element = new AccordionControlElement
                    {
                        Name = menuCode,
                        Text = menuName,
                        Style = ElementStyle.Item
                    };

                    if (string.IsNullOrEmpty(parentCode))
                    {
                        // 최상위 메뉴 (부모가 없는 경우)

                        element.Expanded = true;
                        element.Style = ElementStyle.Group;
                        accordionControl1.Elements.Add(element);
                        parentElements[menuCode] = element;
                    }
                    else if (parentElements.ContainsKey(parentCode))
                    {
                        // 부모가 있는 경우
                        element.Expanded = true;
                        parentElements[parentCode].Elements.Add(element);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("메뉴 로딩 중 오류 발생: " + ex.Message);
            }
