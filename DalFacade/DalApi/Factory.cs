namespace DalApi;
using System.Reflection;
//using DalApi;
using static DalApi.Config;

// מחלקה סטטית המאפשרת ליצור אובייקטים ממחלקות שונות בהתאם להגדרות התצורה של ה- DAL.
public static class Factory
{
    // פרופרטי סטטי המחזיר את האובייקט של ממשק ה- IDal.
    public static IDal Get
    {
        get
        {
            // הבאת שם ה IDAL מהתצורה
            //אם הוא לא קיים תזרק חריגה
            string dalType = s_dalName ?? throw new DalConfigException($"DAL name is not extracted from the configuration");
            // באתר המחלקה DalImplementation המתאימה לשם ה-DAL שהבאנו, אם אינה קיימת ייזרק חריגה.
            DalImplementation dal = s_dalPackages[dalType] ?? throw new DalConfigException($"Package for {dalType} is not found in packages list in dal-config.xml");
            // טעינת ה-assembly שמתאים ל-DAL, אם הוא לא קיים ייזרק חריגה.
            try { Assembly.Load(dal.Package ?? throw new DalConfigException($"Package {dal.Package} is null")); }
            catch (Exception ex) { throw new DalConfigException($"Failed to load {dal.Package}.dll package", ex); }
            // משיגת ה-Type של ה- class המתאים מתוך ה-assembly שנטען.
            Type type = Type.GetType($"{dal.Namespace}.{dal.Class}, {dal.Package}") ??
                throw new DalConfigException($"Class {dal.Namespace}.{dal.Class} was not found in {dal.Package}.dll");

            // משיגת ה-property "Instance" מה-class, אם הוא לא נמצא או שהוא אינו מסוג IDal ייזרקה חריגה.
            return type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) as IDal ??
                throw new DalConfigException($"Class {dal.Class} is not a singleton or wrong property name for Instance");
        }
    }
}

