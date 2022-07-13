using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Homework7.MyHtmlServices;

public static class HtmlHelperExtensions
{
    public static IHtmlContent MyEditorForModel(this IHtmlHelper helper)
    {
        var typeModel = helper.ViewData.ModelMetadata.ModelType;
        var htmlFields = typeModel.GetProperties().Select(p => p.ConvertFieldToHtml(helper.ViewData.Model));
        IHtmlContentBuilder result = new HtmlContentBuilder();
        return htmlFields.Aggregate(result, (current, content) => current.AppendHtml(content));
    }

    private static IHtmlContent ConvertFieldToHtml(this PropertyInfo propertyInfo, object model)
    {
        var div = new TagBuilder("div")
        {
            Attributes = { { "class", "editor-label" } }
        };
        div.InnerHtml.AppendHtml(CreateLabelForTitle(propertyInfo));
        div.InnerHtml.AppendHtml(CreateFieldForInput(propertyInfo, model));
        div.InnerHtml.AppendHtml(MakeSpan(propertyInfo, model));
        return div;
    }

    private static string GetDisplayName(MemberInfo propertyInfo) =>
        propertyInfo.GetCustomAttribute<DisplayAttribute>()?.Name ??
        string.Join(" ", propertyInfo.Name.SplitByCamelCase());

    private static IHtmlContent CreateLabelForTitle(PropertyInfo propertyInfo)
    {
        var label = new TagBuilder("label")
        {
            Attributes =
            {
                { "class", "col-lg-2 col-sm-2 col-form-label" },
                { "for", propertyInfo.Name }
            }
        };

        label.InnerHtml.AppendHtmlLine(GetDisplayName(propertyInfo));
        return label;
    }

    private static string SplitByCamelCase(this string s)
        => s.Skip(1).Aggregate($"{s.FirstOrDefault()}",
            (word, symbol) => word + (char.IsUpper(symbol)
                ? $" {symbol}"
                : symbol));

    private static IHtmlContent CreateDropDown(PropertyInfo propertyInfo, object model)
    {
        var select = new TagBuilder("select")
        {
            Attributes =
            {
                { "id", propertyInfo.Name },
                { "name", propertyInfo.Name }
            }
        };

        var modelValue = model is not null ? propertyInfo.GetValue(model) : 0;
        var memberInfo = propertyInfo.PropertyType
            .GetFields(BindingFlags.Public | BindingFlags.Static);
        foreach (var memInfo in memberInfo)
        {
            var option = CreateVariation(memInfo, modelValue);
            select.InnerHtml.AppendHtml(option);
        }

        return select;
    }

    private static IHtmlContent CreateVariation(FieldInfo memInfo, object modelValue)
    {
        var enumType = memInfo.DeclaringType;
        var option = new TagBuilder("option")
        {
            Attributes =
            {
                { "value", memInfo.Name }
            }
        };
        if (memInfo.GetValue(enumType)?.Equals(modelValue) ?? false)
            option.MergeAttribute("selected", "true");
        option.InnerHtml.AppendHtmlLine(GetDisplayName(memInfo));
        return option;
    }

    private static IHtmlContent CreateFieldForInput(this PropertyInfo propertyInfo, object model)
    {
        var div = new TagBuilder("div");

        div.InnerHtml.AppendHtml(propertyInfo.PropertyType.IsEnum
            ? CreateDropDown(propertyInfo, model)
            : CreateInput(propertyInfo, model));
        return div;
    }

    private static IHtmlContent CreateInput(PropertyInfo propertyInfo, object model)
    {
        var input = new TagBuilder("input")
        {
            Attributes =
            {
                { "class", "form-control w-25" },
                { "id", propertyInfo.Name },
                { "name", propertyInfo.Name },
                { "type", propertyInfo.PropertyType == typeof(int) ? "number" : "text" },
                { "value", model is not null ? propertyInfo.GetValue(model)?.ToString() ?? "" : "" }
            }
        };

        return input;
    }

    public static IHtmlContent MakeSpan(PropertyInfo propertyInfo, object model)
    {
        if (model is null) return null;
        var attributes = propertyInfo.GetCustomAttributes<ValidationAttribute>();
        foreach (var attr in attributes)
        {
            var value = propertyInfo.GetValue(model);
            if (attr.IsValid(value)) continue;
            var span = new TagBuilder("span")
            {
                Attributes =
                {
                    { "class", "text-danger" },
                    { "data-for", "propertyInfo.Name" },
                    { "data-replace", "true" }
                }
            };
            span.InnerHtml.Append(attr.ErrorMessage ?? attr.FormatErrorMessage(propertyInfo.Name)!);
            return span;
        }
        return null;
    }
} 