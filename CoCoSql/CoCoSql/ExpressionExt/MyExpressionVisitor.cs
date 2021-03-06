﻿using CoCoSql.Attributer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CoCoSql.ExpressionExt {
    internal class MyExpressionVisitor : ExpressionVisitor {
        private Stack<string> _SqlStack = new Stack<string>();

        internal string WhereMarkUp<T>() {
            var template = " Where {0} ";
            var sqlWhere = string.Concat(_SqlStack);
            sqlWhere = string.Format(template, sqlWhere);
            return sqlWhere;
        }

        protected override Expression VisitBinary(BinaryExpression node) {
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
        protected override Expression VisitMember(MemberExpression node) {
            try {
                var value = new object();
                var @object = ((ConstantExpression)(node.Expression)).Value; //这个是重点

                if (node.Member.MemberType == MemberTypes.Field) {
                    value = ((FieldInfo)node.Member).GetValue(@object);
                } else if (node.Member.MemberType == MemberTypes.Property) {
                    value = ((PropertyInfo)node.Member).GetValue(@object);
                }
                _SqlStack.Push($"'{value}'");
                return node;
            } catch (InvalidCastException ex) {
                _SqlStack.Push($"[{node.Member.Name}]");
                return node;
            } catch (Exception ex) {
                throw;
            }
        }

        /// <summary>
        /// 常量表达式
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitConstant(ConstantExpression node) {
            _SqlStack.Push($"'{node.Value.ToString()}'");
            return node;
        }

        /// <summary>
        /// 方法调用表达式
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitMethodCall(MethodCallExpression node) {
            string template = string.Empty;
            switch (node.Method.Name) {
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
            string right = _SqlStack.Pop().Replace("'", "");
            string left = _SqlStack.Pop().Replace("'", "");
            _SqlStack.Push(string.Format(template, left, right));
            return node;
        }
    }
}
