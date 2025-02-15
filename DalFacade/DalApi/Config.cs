namespace DalApi;
using System.Xml.Linq;

static class Config
{
    /// <summary>
    /// internal PDS class
    /// </summary>
    internal record DalImplementation
    (
        string Package,   // package/dll name
        string Namespace, // namespace where DAL implementation class is contained in
        string Class   // DAL implementation class name
    );

    internal static string s_dalName;// משתנה סטטי המייצג את שם ה-DAL.
    internal static Dictionary<string, DalImplementation> s_dalPackages;/// מילון סטטי המקשר בין שמות ה-DAL לבין המימושים שלהם.

    static Config()
    {
        // קריאת קובץ ה-DAL מתוך קובץ XML, אם הוא לא קיים יוזרקה חריגה מסוג DalConfigException.
        XElement dalConfig = XElement.Load(@"..\xml\dal-config.xml") ?? throw new DalConfigException("dal-config.xml file is not found");

        // השמת הערך של ה-DAL במשתנה s_dalName, אם הוא ריק יוזרקה חריגה מסוג DalConfigException.
        s_dalName =
           dalConfig.Element("dal")?.Value ?? throw new DalConfigException("<dal> element is missing");

        // קריאת כל החבילות של ה-DAL מתוך קובץ ה-XML, אם הם לא קיימים יוזרקה חריגה מסוג DalConfigException.
        var packages = dalConfig.Element("dal-packages")?.Elements() ?? throw new DalConfigException("<dal-packages> element is missing");
        // אתחול המילון s_dalPackages באמצעות לינק שמשווה בין שמות ה-DAL לבין המימושים שלהם.
        s_dalPackages = (from item in packages
                         let pkg = item.Value
                         let ns = item.Attribute("namespace")?.Value ?? "Dal"
                         let cls = item.Attribute("class")?.Value ?? pkg
                         select (item.Name, new DalImplementation(pkg, ns, cls))
                        ).ToDictionary(p => "" + p.Name, p => p.Item2);
    }
}

[Serializable]
// מחלקה שמייצגת חריגה מסוג DalConfigException, המשמשת לטיפול בשגיאות בתהליך קונפיגורציה של ה-DAL.
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }// בנאי ליצירת חריגה עם הודעה מתאימה
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }//בנאי ליצירת חריגה עם הודעה מתאימה והפניה לחריגה קודמת.
}

