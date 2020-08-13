using CoCoSql.Attributer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CoCoSql.ExpressionExt
{
    internal class MyExpressionVisitor : ExpressionVisitor
    {
        private Stack<string> _SqlStack = new Stack<string>();

        internal string MarkUp<T>()
        {
            var type = typeof(T);
            var objTableAttr = type.GetCustomAttributes(false).FirstOrDefault(x => x is TableAttribute);
            if (objTableAttr == null)
                throw new Exception("无法读取表特性");
            var tableAttr = objTableAttr as TableAttribute;
            var template = "Select * from {0} Where {1} ;";
            var sqlWhere = string.Concat(_SqlStack);
            var sql = string.Format(template, tableAttr.TableName, sqlWhere);
            return sql;
        }
        protected override Expression VisitBinary(BinaryExpression node)
        {
            this.Visit(node.Right);
            _SqlStack.Push(node.NodeType.ConvertToSqlOperator());
            this.Visit(node.Left);
            return node;
        }

        /// <summary>
        /// 成员表达式
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitMember(MemberExpression node)
        {
            _SqlStack.Push($"[{node.Member.Name}]");
            return node;
        }

        /// <summary>
        /// 常量表达式
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitConstant(ConstantExpression node)
        {
            _SqlStack.Push($"'{node.Value.ToString()}'");
            return node;
        }

        /// <summary>
        /// 方法调用表达式
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            string template = string.Empty;
            switch (node.Method.Name)
            {
                case "StartsWith":
                    template = " {0} Like '{1}%' ";
                    break;
                case "EndsWith":
                    template = " {0} Like '%{1}' ";
                    break;
                case "Contains":
                    template = " {0} Like '%{1}%' ";
                    break;
                default:
                    throw new Exception($"{node.Method.Name} 不被支持!");
            }
            this.Visit(node.Object);
            this.Visit(node.Arguments[0]);
            string right = _SqlStack.Pop().Replace("'","");
            string left = _SqlStack.Pop().Replace("'", "");
            _SqlStack.Push(string.Format(template, left, right));
            return node;
        }
    }
}
