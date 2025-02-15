using DalApi;
using System.Collections;
using System.Text;

namespace BO;
// מחלקה סטטית פנימית בשם Tools, המכילה כלים עזר לעבודה עם אובייקטים.
internal static class Tools
{   
    
    // מחלקה סטטית פנימית בשם Tools, המכילה כלי עזר לעבודה עם אובייקטים.

    internal static string ToStringProperty<Item>(this Item item)
        where Item : notnull
    {
        var toString = new StringBuilder();// משתנה המייצר מחרוזת חדשה.
        var properties = item.GetType().GetProperties(); //משתנה המכיל את כל התכונות של האובייקט
        var newLine = Environment.NewLine;// משתנה המכיל את התו המייצג שורה חדשה במחרוזת

        foreach (var property in properties)// לולאה שעוברת על כל התכונות של האובייקט
        {
            var value = property.GetValue(item);// משתנה המכיל את הערך של התכונה הנוכחית
            if (value is not null)
            {
                if (value is IEnumerable enumerable and not string)// בדיקה האם הערך הוא אוסף של אובייקטים (לא מחרוזת)
                {
                    var coolectionName = $"{property.Name}: {newLine}";// משתנה המכיל את שם האוסף
                    toString.AppendLine($"{coolectionName} {newLine}");// הוספת השם של האוסף למחרוזת

                    foreach (var obj in enumerable)
                    {
                        toString.Append(obj);// הוספת התכונה וערכה למחרוזת
                    }

                }
                else
                {
                    toString.AppendLine($"{property.Name}: {value}");
                }
            }
        }

        return toString.ToString();// החזרת המחרוזת המרוכזת שנבנתה
    }  

}
