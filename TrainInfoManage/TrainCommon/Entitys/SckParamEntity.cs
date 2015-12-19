using System;
using System.Collections.Generic;
using System.Text;

namespace TrainCommon
{
    /// <summary>
    /// 封包参数实体类
    /// </summary>
    public class SckParamEntity
    {
        // 模板关键字
        private string _Key = "";

        /// <summary>
        /// 模板关键字
        /// </summary>
        public string Key
        {
            get { return _Key; }
            set { _Key = value; }
        }

        // 模板关键字
        private string[] _Values = null;

        /// <summary>
        /// 模板关键字对应值
        /// </summary>
        public string[] Values
        {
            get { return _Values; }
            set { _Values = value; }
        }

        // 是否内容主体
        private bool _IsBody = false;

        /// <summary>
        /// 是否内容主体
        /// </summary>
        public bool IsBody
        {
            get { return _IsBody; }
            set { _IsBody = value; }
        }

        // 指定值数组一个或多个维度内容作为单一包封装
        private string _SingleIndexs = "";

        /// <summary>
        /// 指定值数组一个或多个维度内容作为单一包封装
        /// </summary>
        public string SingleIndexs
        {
            get { return _SingleIndexs; }
            set { _SingleIndexs = value; }
        }

        /// <summary>
        ///  SckParamEntity构造函数
        /// </summary>
        /// <param name="key">模板关联关键字</param>
        /// <param name="values">关键字对应值数组</param>
        /// <param name="isBody">是否为内容主体，否则为包主题</param>
        public SckParamEntity(string key, string[] values, bool isBody)
        {
            this._Key = key;
            this._Values = values;
            this._IsBody = isBody;
        }

        /// <summary>
        /// SckParamEntity构造函数
        /// </summary>
        /// <param name="key">模板关联关键字</param>
        /// <param name="values">关键字对应值数组</param>
        /// <param name="isBody">是否为内容主体，否则为包主题</param>
        /// <param name="singleIndexs">指定值数组一个或多个维度内容作为单一包封装</param>
        public SckParamEntity(string key, string[] values, bool isBody, string singleIndexs)
        {
            this._Key = key;
            this._Values = values;
            this._IsBody = isBody;
            this._SingleIndexs = singleIndexs;
        }
    }
}
