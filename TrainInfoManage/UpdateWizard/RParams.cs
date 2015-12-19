using System;

using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace UpdateWizard
{
    public class RParams : IDisposable
    { 
        #region 集合变量
        //泛型指定List集合中的子项类型为SqlParameter
        public List<SQLiteParameter> Items = new List<SQLiteParameter>();
        //验证表达式的集合
        public List<EVerification> VerificationList = new List<EVerification>();
        //错误提示消息集合
        public List<int> MsgIndexList = new List<int>();
        //是否允许空值的集合
        public List<bool> EnalbeNull = new List<bool>();
        #endregion

        #region 参数添加的方法的多种重载
        /// <summary>
        /// 增加一个SqlParameter对象(不验证数据格式)
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="value">参数值</param>
        public void Add(string parameterName, object value)
        {
            SQLiteParameter paramItem = new SQLiteParameter();
            paramItem.ParameterName = parameterName;
            paramItem.Value = value;

            this.Items.Add(paramItem);
        }

        /// <summary>
        /// 增加一个SqlParameter对象(不验证数据格式)
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="value">参数值</param>
        /// <param name="dbType">SqlDbType类型</param>
        public void Add(string parameterName, object value, DbType dbType)
        {
            
            SQLiteParameter paramItem = new SQLiteParameter();
            paramItem.ParameterName = parameterName;
            paramItem.Value = value;
            paramItem.DbType = dbType;
            this.Items.Add(paramItem);
        }

        /// <summary>
        /// 增加一个SqlParameter对象(验证数据格式)
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="value">参数值</param>
        /// <param name="ev">验证表达式</param>
        /// <param name="msgIndex">信息索引ID</param>
        /// <returns></returns>
        public bool Add(string parameterName, object value, EVerification ev, int msgIndex)
        {
            return Add(parameterName, value, ev, false, msgIndex);
        }

        /// <summary>
        /// 增加一个SqlParameter对象(验证数据格式)
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="value">参数值</param>
        /// <param name="ev">验证表达式</param>
        /// <param name="enableNull">是否允许为空</param>
        /// <param name="msgIndex">信息索引ID</param>
        /// <returns></returns>
        public bool Add(string parameterName, object value, EVerification ev, bool enableNull, int msgIndex)
        {
            SQLiteParameter paramItem = new SQLiteParameter();
            paramItem.ParameterName = parameterName;
            paramItem.Value = value;

            this.Items.Add(paramItem);
            this.VerificationList.Add(ev);
            this.MsgIndexList.Add(msgIndex);
            this.EnalbeNull.Add(enableNull);

            return true;
        }

        #endregion

        #region 操作扩展的方法
        /// <summary>
        /// 根据名称删除一个子项
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <returns></returns>
        public bool Remove(string parameterName)
        {
            if (GetItemIndex(parameterName) > -1)
            {
                this.Items.Remove(GetSqlParameter(parameterName));
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取指定名称的参数项
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <returns>类型为SqlParameter的参数项</returns>
        public SQLiteParameter GetSqlParameter(string parameterName)
        {
            int index = GetItemIndex(parameterName);
            return index == -1 ? new SQLiteParameter() : this.Items[index];
        }

        /// <summary>
        /// 根据指定的参数名称获取索引
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <returns></returns>
        public int GetItemIndex(string parameterName)
        {
            int index = -1;
            for (int i = 0; i < this.Items.Count; i++)
            {
                if (this.Items[i].ParameterName == parameterName)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
        #endregion


        #region 垃圾回收
        private bool IsDisposed;
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    Items = null;
                    VerificationList = null;
                    MsgIndexList = null;
                    EnalbeNull = null;
                }

                IsDisposed = true;
            }
        }

        ~RParams()
        {
            Dispose(false);
        }
        #endregion

    }
}
