using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using Aregak_Framework_SystemLogger.SystemLogger.NLogLogger;
using Aregak_Framework_SystemLogger.SystemLogger;

namespace Application_DataRepository.DapperDataRepository.Extensions
{
    public static partial class ExpressionToSQLRaw
    {
        #region var
        const string _classFullname = "Aregak_Framework_DataRepository.DapperDataRepository.Extensions.ExpressionToSQLRaw";
        #endregion

        #region prop

        #endregion

        #region ctor

        #endregion

        #region func_public
        public static string ExpressionTreeToString(this Expression expression)
        {
            string _returnedWhereCluase = string.Empty;
            try
            {
               
                    _returnedWhereCluase = ExpressionValue(expression);

              
            }
            catch (Exception ex)
            {
                NlogLoggerClient _client = new NlogLoggerClient(new NLogFileLogger("Aregak_Framework_DataRepository.DapperDataRepository.DataRepository<TEntity>"));
                _client.ActionLoggerAsync(LogginLevels.LoggingLevels.Error, MethodBase.GetCurrentMethod().Name, ex.Message, ex).Wait();
            }
            return _returnedWhereCluase;
        }
        #endregion

        #region func_private
        public static string ExpressionValue(Expression expression)
        {
            StringBuilder _returnedText = new StringBuilder();
            switch (expression.NodeType)
            {
                case ExpressionType.Lambda:
                    var _expression = (LambdaExpression)expression;
                    return ExpressionValue(_expression.Body);

                case ExpressionType.AndAlso:
                case ExpressionType.And:

                    var _expressionAnd = expression as BinaryExpression;
                    return ExpressionValue(_expressionAnd.Left) + " AND " + ExpressionValue(_expressionAnd.Right);

                case ExpressionType.Convert:
                    return GetValueOfConvertExpression(expression);

                case ExpressionType.Equal:
                    var _expressionEqual = expression as BinaryExpression;
                    if (_expressionEqual.Right is ConstantExpression)
                    {
                        var _constantRightExp = (ConstantExpression)_expressionEqual.Right;
                        if (_constantRightExp.Value == null)
                        {
                            return ExpressionValue(_expressionEqual.Left) + " IS " + ExpressionValue(_expressionEqual.Right);
                        }
                    }
                    return ExpressionValue(_expressionEqual.Left) + " = " + ExpressionValue(_expressionEqual.Right);

                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    var _expressionOR = expression as BinaryExpression;
                    return ExpressionValue(_expressionOR.Left) + " OR " + ExpressionValue(_expressionOR.Right);

                case ExpressionType.Constant:
                    var _expressionConstant = expression as ConstantExpression;
                    if (_expressionConstant.Type == typeof(string))
                    {
                        return _expressionConstant.Value == null ? string.Format("NULL"): string.Format("N'{0}'", _expressionConstant.Value);
                    }
                    else
                    {
                        return string.Format("{0}", _expressionConstant.Value);
                    }

                case ExpressionType.MemberAccess:
                    var _expressionMemberAccess = expression as MemberExpression;
                    if (_expressionMemberAccess.Expression.NodeType == ExpressionType.Parameter)
                    {
                        return _expressionMemberAccess.Member.Name;
                    }
                    else
                    {
                     
                        if ((_expressionMemberAccess.Expression as ConstantExpression)!=null)
                        {
                            var _constantExpressField = (ConstantExpression)_expressionMemberAccess.Expression;
                            if (_expressionMemberAccess.Member.MemberType == MemberTypes.Property)
                            {
                                PropertyInfo _memberAccessProperty = (PropertyInfo)_expressionMemberAccess.Member;
                                var _fieldInfoVal = ((FieldInfo)_expressionMemberAccess.Member).GetValue(_constantExpressField.Value);
                                var _propertyVal = _memberAccessProperty.GetValue(_fieldInfoVal, null);
                                if (expression.Type == typeof(string)|| expression.Type == typeof(DateTime) || expression.Type == typeof(Nullable<DateTime>))
                                {
                                    return string.Format("N'{0}'", _propertyVal.ToString());
                                }
                                return _propertyVal.ToString();
                            }
                            else if (_expressionMemberAccess.Member.MemberType == MemberTypes.Field)
                            {

                                if (_constantExpressField != null)
                                {
                                    var _value = ((FieldInfo)_expressionMemberAccess.Member).GetValue(_constantExpressField.Value);
                                    if (expression.Type == typeof(string) || expression.Type == typeof(DateTime)|| expression.Type == typeof(Nullable<DateTime>))
                                    {
                                        return string.Format("N'{0}'", _value.ToString());
                                    }
                                    return _value.ToString();
                                }
                            }
                        }
                        else
                        {
                            ExpressionValue(_expressionMemberAccess.Expression);
                        }
                       
                       
                    }
                    return "";
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                    var _expressionGreaterThan = expression as BinaryExpression;
                    if (_expressionGreaterThan.Right is ConstantExpression)
                    {
                        var _constantRightExp = (ConstantExpression)_expressionGreaterThan.Right;
                        if (_constantRightExp.Value == null)
                        {
                            return ExpressionValue(_expressionGreaterThan.Left) + " IS " + ExpressionValue(_expressionGreaterThan.Right);
                        }
                    }
                    return ExpressionValue(_expressionGreaterThan.Left) + " >= " + ExpressionValue(_expressionGreaterThan.Right);
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                    var _expressionLessThan = expression as BinaryExpression;
                    if (_expressionLessThan.Right is ConstantExpression)
                    {
                        var _constantRightExp = (ConstantExpression)_expressionLessThan.Right;
                        if (_constantRightExp.Value == null)
                        {
                            return ExpressionValue(_expressionLessThan.Left) + " IS " + ExpressionValue(_expressionLessThan.Right);
                        }
                    }
                    return ExpressionValue(_expressionLessThan.Left) + " <= " + ExpressionValue(_expressionLessThan.Right);
                case ExpressionType.Call:
                    var _expressionMethodCall = (MethodCallExpression)expression;
                    if (_expressionMethodCall.Method.Name == "Contains")
                    {
                        return string.Format("like '% {0} %'", ExpressionValue(_expressionMethodCall.Arguments[0]));
                    }
                    else if (_expressionMethodCall.Method.Name == "StartsWith")
                    {
                        return string.Format("like '{0} %'", ExpressionValue(_expressionMethodCall.Arguments[0]));
                    }
                    else if (_expressionMethodCall.Method.Name == "EndsWith")
                    {
                        return string.Format("like '% {0}'", ExpressionValue(_expressionMethodCall.Arguments[0]));
                    }
                    break;


                default:
                    break;
            }
            return "Error";
        }

        private static string GetValueOfConvertExpression(Expression experssion)
        {
            try
            {
                var _unaryExpressionval = experssion as UnaryExpression;
                if (_unaryExpressionval.Operand.NodeType == ExpressionType.MemberAccess)
                {
                    var _memberExpression = experssion as MemberExpression;
                    _memberExpression = _memberExpression ?? (_unaryExpressionval != null ? _unaryExpressionval.Operand as MemberExpression : null);
                    if (_memberExpression != null)
                    {

                        if (_memberExpression.Member.MemberType == MemberTypes.Field)
                        {
                            var _constantMember = (ConstantExpression)_memberExpression.Expression;
                            bool result;
                            if (bool.TryParse(_constantMember.Value.ToString(), out result) == true)
                            {
                                return result ? " 1" : " 0";
                            }
                            else
                            {
                                var _value = ((FieldInfo)_memberExpression.Member).GetValue(_constantMember.Value);
                                if (experssion.Type == typeof(string) || experssion.Type == typeof(DateTime)|| experssion.Type == typeof(Nullable<DateTime>))
                                {
                                    return string.Format("N'{0}'", _value.ToString());
                                }
                                return _value.ToString();
                            }

                        }
                        else if (_memberExpression.Member.MemberType == MemberTypes.Property)
                        {
                            PropertyInfo _expressionProperty = (PropertyInfo)_memberExpression.Member;
                            if (_memberExpression.Expression is ConstantExpression)
                            {
                                var _constantExpression = (ConstantExpression)_memberExpression.Expression;
                                var _expressionFieldObject = ((FieldInfo)_memberExpression.Member).GetValue(_constantExpression.Value);
                                string _result = _expressionProperty.GetValue(_expressionFieldObject, null).ToString();
                            }
                            else
                            {
                                return ExpressionValue(_memberExpression.Expression?? _memberExpression);
                            }
                          
                           
                        }


                    }
                }
                else if (_unaryExpressionval.Operand.NodeType == ExpressionType.Constant)
                {
                    var _constentExpression = ((ConstantExpression)_unaryExpressionval.Operand);
                    bool result;
                    if (bool.TryParse(_constentExpression.Value.ToString(), out result) == true)
                    {
                        return result ? " 1" : " 0";
                    }

                    else
                    {
                        return _constentExpression.Value.ToString();
                    }

                }
                else
                {
                   // return (Expression.Lambda<Func<T>>(experssion).Compile()()).ToString();
                }

                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message+" "+ ex.InnerException.Message);
            }
        }

        #endregion
    }
}
