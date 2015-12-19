using System;

using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TrainCommon
{
    public class SckParams
    {
        //数据包格式样例：#{主题(含命令)+::+内容+::+长度+包数+当前第几包}#

        // 泛型指定Items的List集合中的子项类型为SckParamEntity
        private List<SckParamEntity> ParamItems = new List<SckParamEntity>();

        // 数据包模板
        private string _Tml = "";
        /// <summary>
        /// 数据包模板
        /// </summary>
        public string Tml
        {
            get { return _Tml; }
            set { _Tml = value; }
        }

        // 总包数
        private int _PackCount = 0;
        /// <summary>
        /// 总包数
        /// </summary>
        public int PackCount
        {
            get { return _PackCount; }
        }

        // 生成的包内容集合
        private List<string> _PackItems = new List<string>();
        /// <summary>
        /// 生成的包内容集合
        /// </summary>
        public List<string> PackItems
        {
            get { return _PackItems; }
        }

        // 数据包最大长度，包总长度控制在1400
        private int _PackMaxLength = 1400;
        /// <summary>
        /// 数据包最大长度
        /// </summary>
        public int PackMaxLength
        {
            get { return _PackMaxLength; }
            set { _PackMaxLength = value; }
        }

        // 数据包起始内容
        private string _PackStart = "#{";
        /// <summary>
        /// 数据包起始内容
        /// </summary>
        public string PackStart
        {
            get { return _PackStart; }
            set { _PackStart = value; }
        }

        // 数据包结束内容
        private string _PackEnd = "}#";
        /// <summary>
        /// 数据包结束内容
        /// </summary>
        public string PackEnd
        {
            get { return _PackEnd; }
            set { _PackEnd = value; }
        }

        //包段分隔符
        private string _SplitStr = "$";
        /// <summary>
        /// 包段分隔符
        /// </summary>
        public string SplitStr
        {
            get { return _SplitStr; }
            set { _SplitStr = value; }
        }

        /// <summary>
        /// 增加一个SckParamEntity对象
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <param name="value">参数值</param>
        /// <param name="isBody">是否主体内容</param>
        public void Add(string key, string value, bool isBody)
        {
            SckParamEntity param = new SckParamEntity(key, new string[] { value }, isBody);
            this.ParamItems.Add(param);
        }

        /// <summary>
        /// 增加一个SckParamEntity对象
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <param name="values">参数值</param>
        /// <param name="isBody">是否主体内容</param>
        public void Add(string key, string[] values, bool isBody)
        {
            SckParamEntity param = new SckParamEntity(key, values, isBody);
            this.ParamItems.Add(param);
        }

        /// <summary>
        /// 增加一个SckParamEntity对象
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <param name="value">参数值</param>
        /// <param name="iSmain">是否主体内容</param>
        /// <param name="oneNum">指定值数组一个或多个维度内容作为单一包封装</param>
        public void Add(string key, string[] values, bool isBody, string singleIndexs)
        {
            SckParamEntity param = new SckParamEntity(key, values, isBody, singleIndexs);
            this.ParamItems.Add(param);
        }

        /// <summary>
        /// 移除集合的所有值
        /// </summary>
        public void Clear() {

            ParamItems.Clear();

        }


        public List<string> CreatePacks()
        {
            if (_Tml.Trim() != "")
            {
                //完整数据包
                string pckData = "";
                //模板
                string[] tmlItemArr = _Tml.Split(new char[] { ',' });

                //内容包所在对象索引
                int pckBodyParamIndex = -1;

                //包主题(头)部分
                string pckSubject = "";


                //提示取数据主题与内容
                for (int m = 0; m < tmlItemArr.Length; m++)
                {
                    //遍历参数集合并根据模板生成完整的数据包
                    for (int n = 0; n < ParamItems.Count; n++)
                    {
                        //如果模板关键字与参数键相同，则继续
                        if (ParamItems[n].Key == tmlItemArr[m])
                        {
                            //如果当前参数项是主体内容（即可拆包内容）
                            if (ParamItems[n].IsBody)
                            {
                                //获取包内容列表
                                pckBodyParamIndex = n;
                            }
                            else
                            {
                                //获取包主题(头)
                                pckSubject += ParamItems[n].Values[0] + this._SplitStr;
                            }
                        }
                    }
                }

                //去除包主题最后一个$字符
                if (pckSubject.Length > 0)
                {
                    pckSubject = pckSubject.Substring(0, pckSubject.Length - 1);
                }

                //数据打包
                if (pckBodyParamIndex == -1)
                {
                    return null;
                }
                else
                {
                    //内容部分生成校验码
                    UInt16 crcCode = SckCRC16.Parse(ParamItems[pckBodyParamIndex].Values);
                    this._PackCount = ParamItems[pckBodyParamIndex].Values.Length;
                    
                    for (int i = 0; i < ParamItems[pckBodyParamIndex].Values.Length; i++)
                    {
                        pckData = this._PackStart + pckSubject + "::";
                        pckData += ParamItems[pckBodyParamIndex].Values[i] + "::";

                        pckData += crcCode.ToString("X");
                        pckData += String.Format("{0:00}", this._PackCount);
                        pckData += String.Format("{0:00}", i+1);
                        pckData += this._PackEnd;

                        this._PackItems.Add(pckData);
                    }
                }

                return this._PackItems;
            }
            else
            {
                return null;
            }
        }

    }
}
