using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Trials.Kevin.Common.Ext;
using Trials.Kevin.Contract.Enum;
using Xunit;

namespace Trials.Kevin.XUnitOfTest.FunctionTest
{
    public class FunctionLibraryTest
    {
        [Fact]
        public void GetEnumDescTest()
        {
            LogicDeleteEnum @enum = LogicDeleteEnum.No;

            string desc = @enum.GetEnumDesc();

            System.Console.WriteLine(desc);
        }

        [Fact]
        public void GetEnumValueAndDesc()
        {
            LogicDeleteEnum @enum = LogicDeleteEnum.No;
            Dictionary<int, string> dic = @enum.GetEnumValueAndDesc();
        }

        [Fact]
        public void LinqDynamicSort()
        {
            List<DynamicSortTest> dynamicSortTests = new List<DynamicSortTest>() {
                new DynamicSortTest { Id=1,UserName="zzz",CreateTime=DateTime.Parse("2020-09-10") },
                new DynamicSortTest { Id=2,UserName="xxx",CreateTime=DateTime.Parse("2020-09-09") },
                new DynamicSortTest { Id=3,UserName="ccc",CreateTime=DateTime.Parse("2020-09-09") },
            };

            IQueryable<DynamicSortTest> query = dynamicSortTests.AsQueryable();

            Dictionary<string, string> sortDic = new Dictionary<string, string> {
                {"CreateTime","asc" },
                {"UserName","desc" },
            };

            Type dynamicSortTestType = typeof(DynamicSortTest);

            int index = 0;

            var parameter = Expression.Parameter(dynamicSortTestType, "p");

            foreach (var item in sortDic)
            {
                string callSortMethodName = string.Empty;

                if (string.Compare(item.Value, "asc", true) == 0)
                {
                    if (index == 0)
                    {
                        callSortMethodName = "OrderBy";
                    }
                    else
                    {
                        callSortMethodName = "ThenBy";
                    }
                }
                else
                {
                    if (index == 0)
                    {
                        callSortMethodName = "OrderByDescending";
                    }
                    else
                    {
                        callSortMethodName = "ThenByDescending";
                    }
                }

                PropertyInfo propertyInfo = dynamicSortTestType.GetProperty(item.Key);
                MemberExpression memberExpression = Expression.MakeMemberAccess(parameter, propertyInfo);
                var lambdaExpression = Expression.Lambda(memberExpression, parameter);

                MethodCallExpression methodCallExpression = Expression.Call(typeof(Queryable),
                    callSortMethodName,
                    new Type[] { dynamicSortTestType, propertyInfo.PropertyType },
                    query.Expression, Expression.Quote(lambdaExpression));

                query = query.Provider.CreateQuery<DynamicSortTest>(methodCallExpression);

                index++;
            }

            var result = query.ToList();
        }
    }

    public class DynamicSortTest
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
