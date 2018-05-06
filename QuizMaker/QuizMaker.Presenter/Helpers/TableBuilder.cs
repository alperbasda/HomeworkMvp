using System;
using QuizMaker.Core.AbstractEntity;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace QuizMaker.Presenter.Helpers
{
    public class TableBuilder<T> where T : class, IEntity, new()
    {
        private StringBuilder builder;
        private static char[] _alfabeArray;
        public TableBuilder()
        {
            if (_alfabeArray == null)
            {
                _alfabeArray = new[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'O', 'P' };
            }
            if (builder == null)
            {
                builder = new StringBuilder();
            }
        }

        public StringBuilder CreateHeader(T entity)
        {
            StringBuilder headBuilder = new StringBuilder();
            headBuilder.AppendLine("<thead>");
            foreach (PropertyInfo propertyInfo in entity.GetType().GetProperties().Reverse())
            {
                if (!propertyInfo.Name.ToUpper().Contains("ID") && !propertyInfo.Name.ToUpper().Contains("ANSWER") && !propertyInfo.Name.ToUpper().Contains("MYQUESTİON"))
                {
                    if (!propertyInfo.PropertyType.IsClass)
                    {
                        headBuilder.AppendLine("<th>" + (propertyInfo.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute)?.Name + "</th>");
                    }
                    else if (propertyInfo.PropertyType.ToString().Contains("String"))
                    {
                        headBuilder.AppendLine("<th>" + (propertyInfo.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute)?.Name + "</th>");
                    }
                }
            }

            if (typeof(T).ToString().Contains("Question"))
                headBuilder.AppendLine(CreateAddExamHeader());

            headBuilder.AppendLine(CreateEditHeader());
            headBuilder.AppendLine(CreateRemoveHeader());
            headBuilder.AppendLine("</thead>");
            return headBuilder;
        }

        public StringBuilder CreateBody(IList<T> entityList)
        {
            StringBuilder bodyBuilder = new StringBuilder();
            bodyBuilder.AppendLine("<tbody>");
            foreach (T entity in entityList)
            {
                bodyBuilder.AppendLine(CreateRow(entity));
            }
            bodyBuilder.AppendLine("</tbody>");
            return bodyBuilder;
        }

        public StringBuilder CreateBodyCollapsible(IList<T> entityList)
        {
            StringBuilder bodyBuilder = new StringBuilder();
            bodyBuilder.AppendLine("<tbody>");
            foreach (T entity in entityList)
            {
                bodyBuilder.AppendLine(CreateRowCollapsible(entity));
            }
            bodyBuilder.AppendLine("</tbody>");
            return bodyBuilder;
        }


        public string CreateRowCollapsible(T entity)
        {
            Type t1 = entity.GetType();
            string answers = "";
            string question = "";
            builder.Clear();
            builder.AppendLine("<tr class='first-tr' id='" + t1.GetProperty("Id")?.GetValue(entity) + "' data-toggle='collapse' href='#" + t1.GetProperty("Id")?.GetValue(entity) + "collapse' role='button' aria-expanded='false' aria-controls='deneme' style='cursor: pointer''>");
            foreach (var propertyInfo in t1.GetProperties().Reverse())
            {
                if (propertyInfo.PropertyType.ToString().Contains("Collection"))
                {
                    //Suan için sistem hızı önemsemiyoruz reflection maliyetli bir iş sistemi veri büyüdüğünde yoracaktır
                    IEnumerable values = propertyInfo.GetValue(entity) as IEnumerable;
                    int len = 0;


                    if (values != null)
                    {
                        // ReSharper disable once UnusedVariable
                        foreach (var value in values)
                        {
                            Type t = value.GetType();
                            if (t.ToString().Contains("Answer"))
                            {
                                // ReSharper disable once PossibleNullReferenceException
                                if ((bool)t.GetProperty("IsTrue")?.GetValue(value))
                                {
                                    answers += "<label>" + _alfabeArray[len] + "-) </label><label style='color:green'>&nbsp " + t.GetProperty("MyAnswer")?.GetValue(value) + "</label>";

                                }
                                else
                                {
                                    answers += "<label>" + _alfabeArray[len] + "-) </label><label style='color:red'>&nbsp " + t.GetProperty("MyAnswer")?.GetValue(value) + "</label>";
                                }
                                if (t.ToString().ToUpper().Contains("TRUE"))
                                {
                                    if ((bool)t.GetProperty("IsTrue")?.GetValue(value))
                                    {
                                        try
                                        {
                                            answers += "<label> &nbsp ( " +
                                                       ((Convert.ToInt32(t.GetProperty("TrueAnswerCount")
                                                             ?.GetValue(value)) * 100) /
                                                        Convert.ToInt32(t.GetProperty("AnswerCount")?.GetValue(value)) +
                                                        " %)  </label><br/>");

                                        }
                                        catch (DivideByZeroException e)
                                        {
                                            answers += "<label>&nbsp ( 0 % )</label><br/>";
                                        }
                                    }
                                    else
                                    {
                                        answers += "<br/>";
                                    }
                                }
                                else
                                {
                                    answers += "<br/><label><b>En Yüksek : &nbsp </b> " + t.GetProperty("Highest")?.GetValue(value) + "</label><br/>";
                                    answers += "<label><b>En Düşük : &nbsp </b> " + t.GetProperty("Lowest")?.GetValue(value) + "</label><br/>";
                                    answers += "<label><b>Ortalama : &nbsp </b> " + t.GetProperty("Avarage")?.GetValue(value) + "</label><br/>";

                                }
                            }
                            len++;

                        }
                    }
                    if (values != null && !values.GetType().ToString().Contains("Answer"))
                    {
                        builder.AppendLine("<td class='column-fix'>" + len + "</td>");
                    }
                }
                else
                {
                    if (!propertyInfo.Name.ToUpper().Contains("ID"))
                    {
                        if (!propertyInfo.PropertyType.IsClass)
                        {
                            builder.AppendLine("<td class='column-fix'>" + propertyInfo.GetValue(entity) + "</td>");
                        }
                        else if (propertyInfo.PropertyType.ToString().Contains("String"))
                        {
                            if (!propertyInfo.Name.ToUpper().Contains("MYQUESTİON"))
                            {
                                builder.AppendLine("<td class='column-fix'>" + propertyInfo.GetValue(entity) + "</td>");
                            }
                            else
                            {
                                question = propertyInfo.GetValue(entity).ToString();
                            }
                        }
                    }
                }
            }
            builder.AppendLine(CreateAddExamTag());
            builder.AppendLine(CreateEditTag());
            builder.AppendLine(CreateRemoveTag());
            builder.AppendLine("</tr>");
            builder.AppendLine("<tr class='collapse second-tr' id='" + t1.GetProperty("Id")?.GetValue(entity) + "collapse'><td colspan='8'><label><b>Soru : </b></label><label>&nbsp" + question + "</label><br/>" + answers + "</td></tr>");
            return builder.ToString();
        }


        public string CreateRow(T entity)
        {
            Type t1 = entity.GetType();
            builder.Clear();
            builder.AppendLine("<tr id='" + t1.GetProperty("Id")?.GetValue(entity) + "'>");
            foreach (var propertyInfo in t1.GetProperties().Reverse())
            {
                if (propertyInfo.PropertyType.ToString().Contains("Collection"))
                {
                    //Suan için sistem hızı önemsemiyoruz reflection maliyetli bir iş sistemi veri büyüdüğünde yoracaktır
                    IEnumerable values = propertyInfo.GetValue(entity) as IEnumerable;
                    int len = 0;
                    if (values != null)
                    {
                        // ReSharper disable once UnusedVariable
                        foreach (var value in values)
                        {
                            len++;
                        }
                    }
                    builder.AppendLine("<td class='column-fix'>" + len + "</td>");
                }
                else
                {
                    if (!propertyInfo.Name.ToUpper().Contains("ID"))
                    {
                        if (!propertyInfo.PropertyType.IsClass)
                        {
                            builder.AppendLine("<td class='column-fix'>" + propertyInfo.GetValue(entity) + "</td>");
                        }
                        else if (propertyInfo.PropertyType.ToString().Contains("String"))
                        {
                            builder.AppendLine("<td class='column-fix'>" + propertyInfo.GetValue(entity) + "</td>");
                        }
                    }
                }
            }
            builder.AppendLine(CreateEditTag());
            builder.AppendLine(CreateRemoveTag());
            builder.AppendLine("</tr>");
            return builder.ToString();
        }


        public string CreateRowAjax(T entity)
        {
            Type t1 = entity.GetType();
            builder.Clear();
            builder.AppendLine("<tr id='" + t1.GetProperty("Id")?.GetValue(entity) + "'>");
            foreach (var propertyInfo in t1.GetProperties())
            {
                if (propertyInfo.PropertyType.ToString().Contains("Collection"))
                {
                    //Suan için sistem hızı önemsemiyoruz reflection maliyetli bir iş sistemi veri büyüdüğünde yoracaktır
                    IEnumerable values = propertyInfo.GetValue(entity) as IEnumerable;
                    int len = 0;
                    if (values != null)
                    {
                        // ReSharper disable once UnusedVariable
                        foreach (var value in values)
                        {
                            len++;
                        }
                    }
                    builder.AppendLine("<td>" + len + "</td>");
                }
                else
                {
                    if (!propertyInfo.Name.ToUpper().Contains("ID"))
                    {
                        if (!propertyInfo.PropertyType.IsClass)
                        {
                            builder.AppendLine("<td>" + propertyInfo.GetValue(entity) + "</td>");
                        }
                        else if (propertyInfo.PropertyType.ToString().Contains("String"))
                        {
                            builder.AppendLine("<td>" + propertyInfo.GetValue(entity) + "</td>");
                        }
                    }

                }
            }
            builder.AppendLine(CreateEditTag());
            builder.AppendLine(CreateRemoveTag());
            builder.AppendLine("</tr>");
            return builder.ToString();
        }

        private string CreateRemoveTag()
        {
            return "<td><i class='cursor-point fa fa-trash " + typeof(T).ToString().Split('.')[3] + "Remove' onclick='removeItem(this)'></i></td>";
        }
        private string CreateEditTag()
        {
            return "<td><i class='cursor-point fa fa-edit " + typeof(T).ToString().Split('.')[3] + "Edit' onclick='editItem(this)'></i></td>";
        }
        private string CreateAddExamTag()
        {
            return "<td><i class='cursor-point fa fa-plus " + typeof(T).ToString().Split('.')[3] + "AddExam' onclick='addExamItem(this)'></i></td>";
        }
        private string CreateRemoveHeader()
        {
            return "<th>Sil</th>";
        }
        private string CreateAddExamHeader()
        {
            return "<th>Sınava Ekle</th>";
        }
        private string CreateEditHeader()
        {
            return "<th>Düzenle</th>";
        }
    }
}
