using Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations;
namespace PMTool16Bit
{
    public static class CommonHelpers
    {
        public static bool IsNotNullOrEmpty(this string model)
        {
            return !string.IsNullOrEmpty(model);
        }
        //public static bool IsNullOrEmpty(this string model)
        //{
        //    return string.IsNullOrEmpty(model);
        //}

        public static string ToLowerFirstCharacter(this string model)
        {
            if (model.IsNullOrEmpty())
                return "";
            return Char.ToLowerInvariant(model[0]) + model.Substring(1);
        }

        public static string ToDateString(this DateTime? model, string format = "MMM/dd/yyyy")
        {
            return model == null ? "" : model.Value.ToString(format);
        }
        public static string ToDateString(this DateTime model)
        {
            return model == null ? "" : string.Format("{0:dd/MMM/yyyy}", model);
        }

        public static string ToBase64(this string model)
        {
            if (model.IsNullOrEmpty())
                return null;
            byte[] encodedBytes = System.Text.Encoding.Unicode.GetBytes(model);
            return Convert.ToBase64String(encodedBytes);
        }

        public static string FromBase64(this string model)
        {
            if (model.IsNullOrEmpty())
                return null;
            return Encoding.Unicode.GetString(Convert.FromBase64String(model));
        }

        public static DateTime? ToExact(this string model, string format)
        {
            if (model.IsNullOrEmpty())
                return null;
            DateTime.TryParseExact(model, format, CultureInfo.CurrentCulture, DateTimeStyles.None, out var result);
            if (result == DateTime.MinValue)
                return null;
            return result;
        }

        //public static List<string> GetEnumStringList(object model)
        //{
        //    var properties = GetProperties(model);
        //    return properties.Select(p => p.GetValue(model, null).ToString()).ToList();
        //    //foreach (var p in properties)
        //    //{
        //    //    string name = p.Name;
        //    //    var value = p.GetValue(model, null);
        //    //}

        //}
        //private static PropertyInfo[] GetProperties(object obj)
        //{
        //    return obj.GetType().GetProperties();
        //}

        public static bool HasData(this IEnumerable<object> list)
        {
            return (list != null && list.Any());
        }

        public static DateTime ToDateTime(this double unixTime)
        {
            //DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            //long unixTimeStampInTicks = (long)(unixTime * TimeSpan.TicksPerSecond);
            //return new DateTime(unixStart.Ticks + unixTimeStampInTicks, System.DateTimeKind.Utc);

            //var dateTime = new DateTime(2015, 05, 24, 10, 2, 0, DateTimeKind.Local);
            //var dateTimeOffset = new DateTimeOffset(dateTime);
            //var unixDateTime = dateTimeOffset.ToUnixTimeSeconds();
            return DateTimeOffset.FromUnixTimeSeconds((long)unixTime)
                .DateTime.ToLocalTime();
        }

        public static bool HasData<T>(this IEnumerable<T> list)
        {
            return (list != null && list.Any());
        }

        public static List<string> GetAllPublicConstantValues(this Type type)
        {
            return type
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly)
                .Select(x => (string)x.GetRawConstantValue())
                .ToList();
        }
        public static string ToHumanString(this string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(
                input,
                "([^^])([A-Z])",
                "$1 $2"
            );
        }
        
        public static string Join(this IEnumerable<string> model, string separator)
        {
            if (model == null)
            {
                return "";
            }

            return string.Join(separator, model);
        }

        public static DateTime GetPreviousWeekDay(this DateTime currentDate, DayOfWeek dow, int weekCount = 0)
        {
            int currentDay = (int)currentDate.DayOfWeek, gotoDay = (int)dow;
            return currentDate.AddDays(-7).AddDays(gotoDay - currentDay).AddDays(-7 * weekCount);
        }

        public static DateTime GetNextWeekDay(this DateTime currentDate, DayOfWeek dow, int weekCount = 0)
        {
            int currentDay = (int)currentDate.DayOfWeek, gotoDay = (int)dow;
            return currentDate.AddDays(7).AddDays(gotoDay - currentDay).AddDays(7 * weekCount);
        }
    }

    public static class IQueryableExtensions
    {
        private static readonly TypeInfo QueryCompilerTypeInfo = typeof(QueryCompiler).GetTypeInfo();

        private static readonly FieldInfo QueryCompilerField = typeof(EntityQueryProvider).GetTypeInfo().DeclaredFields.First(x => x.Name == "_queryCompiler");

        private static readonly FieldInfo QueryModelGeneratorField = QueryCompilerTypeInfo.DeclaredFields.First(x => x.Name == "_queryModelGenerator");

        private static readonly FieldInfo DataBaseField = QueryCompilerTypeInfo.DeclaredFields.Single(x => x.Name == "_database");

        private static readonly PropertyInfo DatabaseDependenciesField = typeof(Database).GetTypeInfo().DeclaredProperties.Single(x => x.Name == "Dependencies");

        public static string ToSql<TEntity>(this IQueryable<TEntity> query) where TEntity : class
        {
            var queryCompiler = (QueryCompiler)QueryCompilerField.GetValue(query.Provider);
            var modelGenerator = (QueryModelGenerator)QueryModelGeneratorField.GetValue(queryCompiler);
            var queryModel = modelGenerator.ParseQuery(query.Expression);
            var database = (IDatabase)DataBaseField.GetValue(queryCompiler);
            var databaseDependencies = (DatabaseDependencies)DatabaseDependenciesField.GetValue(database);
            var queryCompilationContext = databaseDependencies.QueryCompilationContextFactory.Create(false);
            var modelVisitor = (RelationalQueryModelVisitor)queryCompilationContext.CreateQueryModelVisitor();
            modelVisitor.CreateQueryExecutor<TEntity>(queryModel);
            var sql = modelVisitor.Queries.First().ToString();

            return sql;
        }
    }
}
