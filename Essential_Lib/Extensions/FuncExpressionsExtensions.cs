using System.Linq.Expressions;
using System.Reflection;

namespace Essential_Lib.Extensions
{
    public enum MethodInfoEnums
    {
        StringToLower,
        StringContains,
        StringStartsWith, 
        ArrayAll,
        Null,
    }
    public static class FuncExpressionsExtensions
    {
       public static MethodInfo? EnumToMethodInfo(this MethodInfoEnums infoEnums)
        {
            return infoEnums switch
            {
                MethodInfoEnums.StringToLower=> StringToLowerMethod,
                MethodInfoEnums.StringStartsWith => StringStartsWithMethod,
                MethodInfoEnums.StringContains => StringContainsMethod,

                //MethodInfoEnums.ArrayAll => ArrayAllMethod,
                _ => null,
            };
        }

 
        public static MethodInfo? StringToLowerMethod => typeof(string).GetMethod(nameof(string.ToLower), Type.EmptyTypes);
        public static MethodInfo? StringContainsMethod => typeof(string).GetMethod(nameof(string.Contains), [typeof(string)]); 
        public static MethodInfo? StringStartsWithMethod => typeof(string).GetMethod(nameof(string.StartsWith), [typeof(string)]);

        //private static MethodInfo stringMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);

        //private static readonly MethodInfo StringContainsMethod = typeof(string).GetMethod("Contains", Type.EmptyTypes);
        //private static readonly MethodInfo ArrayAllMethod = typeof(Array).GetMethod("All", Type.EmptyTypes);
         
        public static Expression<Func<T,bool>> FuncToExpression<T>(this T? t,string FilterPropertyName, object FilterValueToCompare,MethodInfo? methodInfo = null,MethodInfoEnums infoEnums = MethodInfoEnums.Null)
        {
            methodInfo ??= infoEnums.EnumToMethodInfo();
            ParameterExpression argParam = Expression.Parameter(typeof(T), "t");
            
            Expression property = Expression.Property(argParam, FilterPropertyName);
            if(FilterValueToCompare is string stringValue)
            {
                FilterValueToCompare = stringValue.ToLowerInvariant();
            }
            ConstantExpression val  = Expression.Constant(FilterValueToCompare, FilterValueToCompare==null? typeof(string): FilterValueToCompare.GetType());
            Expression method; 
            ConstantExpression trueVal = val;
            if (infoEnums != MethodInfoEnums.StringToLower && infoEnums != MethodInfoEnums.Null)
            {
                trueVal = Expression.Constant(true, typeof(bool));
                Expression lower = Expression.Call(property, StringToLowerMethod);
                //method = methodInfo == null ? property : Expression.Call(argParam, methodInfo, val, Expression.Constant(StringComparison.InvariantCultureIgnoreCase));
                method = methodInfo == null ? property : Expression.Call(lower, methodInfo, val ); 
            }
            else
            {
                method = methodInfo == null ? property : Expression.Call(property, methodInfo); 
            }
            var body = Expression.Equal(method, trueVal);

            //var nullCheck = Expression.NotEqual(property, Expression.Constant(null)); 
            // var body = Expression.AndAlso(nullCheck, condition); 
           return Expression.Lambda<Func<T, bool>>(body, argParam); 
        }
        public static Expression<Func<T, bool>> FuncToExpression<T>(this string FilterPropertyName, object[] FilterValuesToCompare, MethodInfoEnums infoEnums = MethodInfoEnums.Null,bool UseLogicalOr = true)
        {
            MethodInfo? methodInfo = infoEnums.EnumToMethodInfo();
            ParameterExpression argParam = Expression.Parameter(typeof(T), "t");
            Expression property = Expression.Property(argParam, FilterPropertyName);
            //var nullConstant = Expression.Constant(null, typeof(object));
            //Expression.NotEqual(property, nullConstant)
            // Create the initial expression
            Expression? combinedExpression = null;
            FilterValuesToCompare ??= [];
            for (var i=0; i< FilterValuesToCompare.Length; i++)
            {
                Expression currentExpression;
                if (FilterValuesToCompare[i] is string stringValue)
                {
                    FilterValuesToCompare[i] = stringValue.ToLowerInvariant();
                    methodInfo ??= StringToLowerMethod;
                }
                ConstantExpression val = Expression.Constant(FilterValuesToCompare[i], FilterValuesToCompare[i] == null ? typeof(string) : FilterValuesToCompare[i].GetType());

                if (infoEnums != MethodInfoEnums.StringToLower && infoEnums != MethodInfoEnums.Null)
                {
                    Expression lower = Expression.Call(property, StringToLowerMethod);
                    currentExpression = methodInfo == null ? property : Expression.Call(lower, methodInfo, val);
                }
                else
                {
                    currentExpression = methodInfo == null ? property : Expression.Call(property, methodInfo);
                }

                var body = Expression.Equal(currentExpression,  infoEnums != MethodInfoEnums.StringContains ? val: Expression.Constant(true, typeof(bool)));

                // Combine the expressions using OR
                combinedExpression = combinedExpression == null ? body : (UseLogicalOr? Expression.OrElse(combinedExpression, body) : Expression.AndAlso(combinedExpression, body));
            }
            // Return a default expression that always returns false
            combinedExpression ??= Expression.Constant(false);
            return Expression.Lambda<Func<T, bool>>(combinedExpression, argParam);
        }
        //public static Expression<Func<T, bool>> FuncToExpression<T>(this IEnumerable<string> searchStrings)
        //{
        //    var parameter = Expression.Parameter(typeof(T), "x");
        //    var nameProperty = Expression.Property(parameter, "Name");
        //    var availableCountriesProperty = Expression.Property(parameter, "AvailableContriesBloobedText");

        //    Expression combinedExpression = null;

        //    foreach (var searchString in searchStrings)
        //    {
        //        var nameContains = Expression.Call(
        //            nameProperty,
        //            "Contains",
        //            Type.EmptyTypes,
        //            Expression.Constant(searchString)
        //        );

        //        var availableCountriesContains = Expression.Call(
        //            availableCountriesProperty,
        //            "Contains",
        //            Type.EmptyTypes,
        //            Expression.Constant(searchString)
        //        );

        //        var orExpression = Expression.OrElse(nameContains, availableCountriesContains);

        //        combinedExpression = combinedExpression == null
        //            ? orExpression
        //            : Expression.OrElse(combinedExpression, orExpression);
        //    }

        //    return Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);
        //}
    }

}
