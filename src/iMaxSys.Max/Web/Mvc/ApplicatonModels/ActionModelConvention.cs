//----------------------------------------------------------------
//Copyright (C) 2016-2022 iMaxSys Co.,Ltd.
//All rights reserved.
//
//文件: ApiExplorerPostOnlyConvention.cs
//摘要: ApiExplorerPostOnlyConvention
//说明:
//
//当前：1.0
//作者：陶剑扬
//日期：2017-11-15
//----------------------------------------------------------------

namespace iMaxSys.Max.Web.Mvc.ApplicatonModels;

/// <summary>
/// ApiExplorerPostOnlyConvention
/// </summary>
public class ApiExplorerPostOnlyConvention : IActionModelConvention
{
    public static void Apply(ActionModel action)
    {
        //action.ApiExplorer.IsVisible = action.Attributes.OfType<HttpPostAttribute>().Any();
    }
}

/// <summary>
/// 默认FromBody参数绑定
/// </summary>
public class ActionModelConventionOnlyFromBody : IActionModelConvention
{
    public static void Apply(ActionModel action)
    {
        if (action == null)
        {
            throw new ArgumentNullException(nameof(action));
        }

        //action.ApiExplorer.IsVisible = action.Attributes.OfType<HttpGetAttribute>().Any();


        foreach (var parameter in action.Parameters)
        {
            //if (typeof(RequestObject).IsAssignableFrom((parameter.ParameterInfo.ParameterType)))
            if (parameter.ParameterInfo.ParameterType.IsClass && !parameter.ParameterInfo.ParameterType.IsSealed)
            {
                parameter.BindingInfo = parameter.BindingInfo ?? new BindingInfo();
                parameter.BindingInfo.BindingSource = BindingSource.Body;
            }
        }
    }
}
