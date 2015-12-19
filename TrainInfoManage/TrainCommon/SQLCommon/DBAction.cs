using System;
using System.Text;
using System.Collections.Generic;

using System.Reflection;
using System.Data;
using System.Windows.Forms;

namespace TrainCommon
{
    public class DBAction
    {

        #region 获取数据列表
        /// <summary>
        /// 根据SQL语句获取DataTable
        /// </summary>
        /// <param name="StrSQL">SQL语句</param>
        /// <returns></returns>
        public static DataTable GetDTFromSQL(string StrSQL)
        {

            return SQLiteHelper.Query(StrSQL).Tables[0];
        }

        /// <summary>
        /// 根据SQL语句获取DataSet
        /// </summary>
        /// <param name="StrSQL">SQL语句</param>
        /// <param name="VParams">RParams类型的参数集合</param>
        /// <returns></returns>
        public static DataSet GetDSFromSQLTo(string StrSQL)
        {
            return SQLiteHelper.Query(StrSQL);
        }

        /// <summary>
        /// 根据SQL语句与参数获取DataTable
        /// </summary>
        /// <param name="StrSQL">SQL语句</param>
        /// <param name="VParams">RParams类型的参数集合</param>
        /// <returns></returns>
        public static DataTable GetDTFromSQL(string StrSQL, RParams VParams)
        {
            return SQLiteHelper.Query(StrSQL,VParams.Items.ToArray()).Tables[0];
        }


        /// <summary>
        /// 根据SQL语句与参数获取DataSet
        /// </summary>
        /// <param name="StrSQL">SQL语句</param>
        /// <param name="VParams">RParams类型的参数集合</param>
        /// <returns></returns>
        public static DataSet GetDSFromSQL(string StrSQL, RParams VParams)
        {
            return SQLiteHelper.Query(StrSQL, VParams.Items.ToArray());
        }


        /// <summary>
        /// 根据表、列、条件与参数获取DataTable
        /// </summary>
        /// <param name="TBName">表名</param>
        /// <param name="Columns">列字段</param>
        /// <param name="WhereFields">条件</param>
        /// <param name="VParams">RParams类型的参数集合</param>
        /// <returns></returns>
        public static DataTable GetDTFromSQL(string TBName, string Columns, string WhereFields, RParams VParams)
        {
            string strSQL = "Select " + Columns + " From " + TBName + " Where " + WhereFields;
            return GetDTFromSQL(strSQL, VParams);
        }

        /// <summary>
        /// 根据表、列、条件与参数获取DataSet
        /// </summary>
        /// <param name="TBName">表名</param>
        /// <param name="Columns">列字段</param>
        /// <param name="WhereFields">条件</param>
        /// <param name="VParams">RParams类型的参数集合</param>
        /// <returns></returns>
        public static DataSet GetDSFromSQL(string TBName, string Columns, string WhereFields, RParams VParams)
        {
            string strSQL = "Select " + Columns + " From " + TBName + " Where " + WhereFields;
            return GetDSFromSQL(strSQL, VParams);
        }

        /// <summary>
        /// 根据ETableName中定义的表名获取DataTable
        /// </summary>
        /// <param name="TBName">ETableName类型的表名</param>
        /// <returns></returns>
        public static DataTable GetDTFromTBName(ETableName TBName)
        {
            return GetDTFromTBName(TBName.ToString());
        }

        /// <summary>
        /// 根据ETableName中定义的表名获取DataSet
        /// </summary>
        /// <param name="TBName">ETableName类型的表名</param>
        /// <returns></returns>
        public static DataSet GetDSFromTBName(ETableName TBName)
        {
            return GetDSFromTBName(TBName.ToString());
        }

        /// <summary>
        /// 根据表名字符串获取DataTable
        /// </summary>
        /// <param name="TBName">表名</param>
        /// <returns></returns>
        public static DataTable GetDTFromTBName(string TBName)
        {
            string strSQL = "Select * From " + TBName;
            return SQLiteHelper.Query(strSQL).Tables[0];
        }

        /// <summary>
        /// 根据表名字符串获取DataTable
        /// </summary>
        /// <param name="TBName">表名</param>
        /// <returns></returns>
        public static DataSet GetDSFromTBName(string TBName)
        {
            TBName = "Select * From " + TBName;
            return SQLiteHelper.Query(TBName);
        }
        #endregion

        #region 数据翻页
        /// <summary>
        /// 数据翻页
        /// </summary>
        /// <param name="TBName">枚举数据表名</param>
        /// <param name="Columns">查询列</param>
        /// <param name="Where">查询条件</param>
        /// <param name="OrderBy">排序</param>
        /// <param name="CurrPage">当前页</param>
        /// <param name="PageSize">页行数</param>
        /// <returns></returns>
        public static DataTable GetPageDT(ETableName TBName,string Columns, string Where, int CurrPage, int PageSize)
        {
            return GetPageDT(TBName.ToString(),Columns,Where,CurrPage,PageSize);
        }

        public static DataTable GetPageDT(string TBName, string Columns, string Where,int CurrPage, int PageSize)
        {
            string strWhere = "";
            if (Where!=null&&Where.Trim().Length>0)
            {
                strWhere += " Where " + Where;
            }
            string queryStr = "select " + Columns + " from " + TBName + strWhere + " limit " + (CurrPage - 1) * PageSize + "," + PageSize;
            return SQLiteHelper.Query(queryStr).Tables[0];
        }

        /// <summary>
        /// 数据翻页---降序
        /// </summary>
        /// <param name="TBName">枚举数据表名</param>
        /// <param name="Columns">查询列</param>
        /// <param name="Where">查询条件</param>
        /// <param name="CurrPage">当前页</param>
        /// <param name="PageSize">页行数</param>
        /// <param name="order">排序</param>
        /// <returns></returns>
        public static DataTable GetPageDT(ETableName TBName, string Columns, string Where, int CurrPage, int PageSize, string order)
        {
            return GetPageDT(TBName.ToString(), Columns, Where, CurrPage, PageSize, order);
        }

        public static DataTable GetPageDT(string TBName, string Columns, string Where, int CurrPage, int PageSize,string order)
        {
            string strWhere = "";
            if (Where != null && Where.Trim().Length > 0)
            {
                strWhere += " Where " + Where;
            }
            string queryStr = "select " + Columns + " from " + TBName + strWhere + " order by " + order + " desc " + " limit " + (CurrPage - 1) * PageSize + "," + PageSize;
            return SQLiteHelper.Query(queryStr).Tables[0];
        }

        #endregion

        #region 添加数据
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="TBName">ETableName中定义的表名</param>
        /// <param name="VParams">RParams类型的参数集合</param>
        /// <returns></returns>
        public static bool Insert(ETableName TBName, RParams VParams)
        {
            return Insert(TBName.ToString(), VParams);
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="TBName">表名</param>
        /// <param name="Fields">列名字符串</param>
        /// <param name="VParams">RParams类型的参数集合</param>
        /// <returns></returns>
        public static bool Insert(string TBName, RParams VParams)
        {
            string[] FieldsAndParams = FieldsAndParamsName(VParams);
            string strSQL = "";
            strSQL = "Insert Into " + TBName + "(" + FieldsAndParams[0] + ") Values(" + FieldsAndParams[1] + ")";
            if (SQLiteHelper.ExecuteSql(strSQL, VParams.Items.ToArray())> 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 添加数据并返回ID
        /// <summary>
        /// 添加数据返回数据ID
        /// </summary>
        /// <param name="TBName">ETableName中定义的表名</param>
        /// <param name="Fields">列名字符串</param>
        /// <param name="VParams">RParams类型的参数集合</param>
        /// <returns></returns>
        public static int InsertReturnID(ETableName TBName, RParams VParams)
        {
            return InsertReturnID(TBName.ToString(), VParams);
        }

        /// <summary>
        /// 添加数据返回数据ID
        /// </summary>
        /// <param name="TBName">表名</param>
        /// <param name="Fields">列名字符串</param>
        /// <param name="VParams">RParams类型的参数集合</param>
        /// <returns></returns>
        public static int InsertReturnID(string TBName, RParams VParams)
        {
            string[] FieldsAndParams = FieldsAndParamsName(VParams);
            string strSQL = "";
            strSQL = "Insert Into " + TBName + "(" + FieldsAndParams[0] + ") Values(" + FieldsAndParams[1] + ")";



            int ID = SQLiteHelper.ExecuteSqlReturnLastRowId(strSQL,TBName,"ID");
            return ID;
        }
        #endregion

        #region 修改数据
        /// <summary>
        /// 更新修改数据
        /// </summary>
        /// <param name="TBName">ETableName中定义的表名</param>
        /// <param name="Fields">列名字符串</param>
        /// <param name="VParams">RParams类型的参数集合</param>
        /// <param name="WhereFields">条件</param>
        /// <returns></returns>
        public static bool Update(ETableName TBName, string Fields, string WhereFields, RParams VParams)
        {
            return Update(TBName.ToString(), Fields, WhereFields, VParams);
        }

        /// <summary>
        /// 更新修改数据
        /// </summary>
        /// <param name="TBName">表名</param>
        /// <param name="Fields">列名字符串</param>
        /// <param name="VParams">RParams类型的参数集合</param>
        /// <param name="WhereFields">条件</param>
        /// <returns></returns>
        public static bool Update(string TBName, string Fields, string WhereFields, RParams VParams)
        {
            string valParamRelation = FieldsToRelation(Fields);
            string strSQL = "Update " + TBName + " Set " + valParamRelation + " Where " + WhereFields;

            if (SQLiteHelper.ExecuteSql(strSQL, VParams.Items.ToArray()) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 删除数据
        /// <summary>
        /// 删除数据，自定义条件语句
        /// </summary>
        /// <param name="TBName">ETableName中定义的表名</param>
        /// <param name="WhereFields">条件语句</param>
        /// <returns></returns>
        public static bool Delete(ETableName TBName, string WhereFields)
        {
            return Delete(TBName.ToString(), WhereFields);
        }

        /// <summary>
        /// 删除数据，自定义条件语句
        /// </summary>
        /// <param name="TBName">表名</param>
        /// <param name="WhereField">条件语句</param>
        /// <returns></returns>
        public static bool Delete(string TBName, string WhereFields)
        {
            string strSQL = "Delete from " + TBName;
            if (WhereFields != "")
            {
                strSQL += " Where " + WhereFields;

            }
            if (SQLiteHelper.ExecuteSql(strSQL) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除数据，自定义参数条件与参数集合
        /// </summary>
        /// <param name="TBName">ETableName中定义的表名</param>
        /// <param name="VParams">RParams类型的参数集合</param>
        /// <param name="WhereFields">参数条件</param>
        /// <returns></returns>
        public static bool Delete(ETableName TBName, string WhereFields, RParams VParams)
        {
            return Delete(TBName.ToString(), WhereFields, VParams);
        }

        /// <summary>
        /// 删除数据，自定义参数条件与参数集合
        /// </summary>
        /// <param name="TBName">表名</param>
        /// <param name="VParams">RParams类型的参数集合</param>
        /// <param name="WhereFields">参数条件</param>
        /// <returns></returns>
        public static bool Delete(string TBName, string WhereFields, RParams VParams)
        {
            string strSQL = "Delete from " + TBName;
            if (WhereFields!="")
            {
                strSQL+=" Where " + WhereFields;

            }
            if (SQLiteHelper.ExecuteSql(strSQL, VParams.Items.ToArray()) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 获取记录总数
        public static int GetRecordCount(ETableName TBName, string WhereFields, RParams VParams)
        {
            return GetRecordCount(TBName.ToString(), WhereFields, VParams);
        }
        /// <summary>
        /// 获取记录总数
        /// </summary>
        /// <param name="TBList">表名列表，如：Table1 t1,Table2,t2</param>
        /// <param name="strWhere">Where查询条件</param>
        /// <returns>返回记录数</returns>
        public static int GetRecordCount(string TBList, string WhereFields, RParams VParams)
        {
            string strSQL = "select count(*) from " + TBList;
            if (WhereFields != "")
            {
                strSQL += " where " + WhereFields;
            }

            return Convert.ToInt32(SQLiteHelper.Query(strSQL, VParams.Items.ToArray()).Tables[0].Rows[0][0]);
        }

        /// <summary>
        /// 获取总页数
        /// </summary>
        /// <param name="TBName">数据表枚举</param>
        /// <param name="WhereFields">查询条件</param>
        /// <param name="VParams">参数值数组</param>
        /// <param name="PageSize">页行数</param>
        /// <returns></returns>
        public static int GetPageCount(ETableName TBName, string WhereFields, RParams VParams,int PageSize){

            return GetPageCount(TBName.ToString(),WhereFields,VParams,PageSize);

        }

        /// <summary>
        /// 获取总页数
        /// </summary>
        /// <param name="TBList">数据表名称</param>
        /// <param name="WhereFields">查询条件</param>
        /// <param name="VParams">参数值数组</param>
        /// <param name="PageSize">页行数</param>
        /// <returns></returns>
        public static int GetPageCount(string TBList, string WhereFields, RParams VParams, int PageSize)
        {
            if (PageSize==0)
            {
                return 0;
            }
            else
            {
                int count = GetRecordCount(TBList, WhereFields, VParams);//数据总条数
                int pageCount = count/PageSize;
                if (count%pageCount>0)
                {
                    pageCount = pageCount + 1;
                }
                return pageCount;
            }
        }


        #endregion

        #region 判断数据是否存在
        /// <summary>
        /// 判断是否有重复数据(存在true,不存在false)
        /// </summary>
        /// <param name="TBList">表名列表，如：Table1 t1,Table2,t2</param>
        /// <param name="strWhere">Where查询条件</param>
        /// <returns>返回数据是否存在</returns>
        public static bool HasData(string TBList, string WhereFields, RParams VParams)
        {
            if (GetRecordCount(TBList, WhereFields, VParams) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 根据列名创建参数名称列表，常用于添加数据
        /// </summary>
        /// <param name="VParams">RParams类型的参数集合</param>
        /// <returns>返回创建后的参数名称列表（字段:a,b,c,...  值@a,@b,@c,...）</returns>
        private static string[] FieldsAndParamsName(RParams VParams)
        {
            string FieldList = "";
            string VParamList = "";

            for (int n = 0; n < VParams.Items.Count; n++)
            {
                FieldList += VParams.Items[n].ParameterName + ",";
                VParamList += "@" + VParams.Items[n].ParameterName + ",";
            }

            FieldList = FieldList.Substring(0, FieldList.Length - 1);
            VParamList = VParamList.Substring(0, VParamList.Length - 1);

            return new string[] { FieldList, VParamList };
        }

        /// <summary>
        /// 根据列名创建修改字段与值参数的关系，常用于修改数据
        /// </summary>
        /// <param name="Fields">绑定字段字符串</param>
        /// <returns>返回创建后的修改字段与值参数的关系（a=@a,b=@b,...）</returns>
        private static string FieldsToRelation(string Fields)
        {
            if (Fields == "")
            {
                return Fields;
            }

            string[] FieldsArr = Fields.Split(',');
            Fields = "";
            for (int n = 0; n < FieldsArr.Length; n++)
            {
                Fields += FieldsArr[n] + "=@" + FieldsArr[n] + ",";
            }
            Fields = Fields.Substring(0, Fields.Length - 1);
            return Fields;
        }
        #endregion

    }
}
