using System.Xml.Linq;

namespace Dal;

internal static class Config
{
    public static readonly string s_data_config_xml = "data-config";
    internal static int NextDependencyId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextDependencyId");
                                          set => XMLTools.SetNextId(s_data_config_xml, "NextDependencyId", value); }
    internal static int NextTaskId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextTaskId");
                                     set => XMLTools.SetNextId(s_data_config_xml, "NextTaskId", value);
    }
    public static DateTime? StartProjectDate
    {
        //get => XMLTools.ToDateTimeNullable(XMLTools.LoadListFromXMLElement(s_data_config_xml),
        //        nameof(StartProjectDate));

        //set
        //{
        //    var root = XMLTools.LoadListFromXMLElement(s_data_config_xml);
        //    root.Element(nameof(StartProjectDate))!.Value
        //        = value.ToString()!;

        //    XMLTools.SaveListToXMLElement(root, s_data_config_xml);
        //}
        get => XMLTools.LoadProjectDate();
        set => XMLTools.SaveProjectDate(value);
    }

    public static DateTime Clock
    {
        get => XMLTools.LoadClock();
        set => XMLTools.SaveClock(value);
    }





}