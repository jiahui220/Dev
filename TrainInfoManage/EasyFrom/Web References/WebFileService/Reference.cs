﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.6413
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.CompactFramework.Design.Data 2.0.50727.6413 版自动生成。
// 
namespace TrainView.WebFileService {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="BasicHttpBinding_IFileTransportService", Namespace="http://tempuri.org/")]
    public partial class FileTransportService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        /// <remarks/>
        public FileTransportService() {
            this.Url = "http://123.85.195.43:8732/MAP/FileTransportService/";
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IFileTransportService/GetFileList", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlArrayAttribute(IsNullable=true)]
        [return: System.Xml.Serialization.XmlArrayItemAttribute(Namespace="http://schemas.microsoft.com/2003/10/Serialization/Arrays")]
        public string[] GetFileList() {
            object[] results = this.Invoke("GetFileList", new object[0]);
            return ((string[])(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetFileList(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetFileList", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public string[] EndGetFileList(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string[])(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IFileTransportService/LogProFilePath", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void LogProFilePath([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string proName, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string verSion, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string upPerson, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string upConment, out int LogProFilePathResult, [System.Xml.Serialization.XmlIgnoreAttribute()] out bool LogProFilePathResultSpecified) {
            object[] results = this.Invoke("LogProFilePath", new object[] {
                        proName,
                        verSion,
                        upPerson,
                        upConment});
            LogProFilePathResult = ((int)(results[0]));
            LogProFilePathResultSpecified = ((bool)(results[1]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginLogProFilePath(string proName, string verSion, string upPerson, string upConment, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("LogProFilePath", new object[] {
                        proName,
                        verSion,
                        upPerson,
                        upConment}, callback, asyncState);
        }
        
        /// <remarks/>
        public void EndLogProFilePath(System.IAsyncResult asyncResult, out int LogProFilePathResult, out bool LogProFilePathResultSpecified) {
            object[] results = this.EndInvoke(asyncResult);
            LogProFilePathResult = ((int)(results[0]));
            LogProFilePathResultSpecified = ((bool)(results[1]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IFileTransportService/UploadFile", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string UploadFile([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string FileName, [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", IsNullable=true)] byte[] fs, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string SendPerSon, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string FileType, int ProID, [System.Xml.Serialization.XmlIgnoreAttribute()] bool ProIDSpecified) {
            object[] results = this.Invoke("UploadFile", new object[] {
                        FileName,
                        fs,
                        SendPerSon,
                        FileType,
                        ProID,
                        ProIDSpecified});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginUploadFile(string FileName, byte[] fs, string SendPerSon, string FileType, int ProID, bool ProIDSpecified, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("UploadFile", new object[] {
                        FileName,
                        fs,
                        SendPerSon,
                        FileType,
                        ProID,
                        ProIDSpecified}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndUploadFile(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IFileTransportService/DeleteFile", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string DeleteFile(int id, [System.Xml.Serialization.XmlIgnoreAttribute()] bool idSpecified) {
            object[] results = this.Invoke("DeleteFile", new object[] {
                        id,
                        idSpecified});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginDeleteFile(int id, bool idSpecified, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("DeleteFile", new object[] {
                        id,
                        idSpecified}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndDeleteFile(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IFileTransportService/DeleteImgFile", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string DeleteImgFile(int id, [System.Xml.Serialization.XmlIgnoreAttribute()] bool idSpecified) {
            object[] results = this.Invoke("DeleteImgFile", new object[] {
                        id,
                        idSpecified});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginDeleteImgFile(int id, bool idSpecified, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("DeleteImgFile", new object[] {
                        id,
                        idSpecified}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndDeleteImgFile(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IFileTransportService/GetFile", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] GetFile([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string fileName) {
            object[] results = this.Invoke("GetFile", new object[] {
                        fileName});
            return ((byte[])(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetFile(string fileName, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetFile", new object[] {
                        fileName}, callback, asyncState);
        }
        
        /// <remarks/>
        public byte[] EndGetFile(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((byte[])(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IFileTransportService/UpCEFile", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string UpCEFile([System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", IsNullable=true)] byte[] fs, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string FileName) {
            object[] results = this.Invoke("UpCEFile", new object[] {
                        fs,
                        FileName});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginUpCEFile(byte[] fs, string FileName, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("UpCEFile", new object[] {
                        fs,
                        FileName}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndUpCEFile(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IFileTransportService/DownLoad", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", IsNullable=true)]
        public byte[] DownLoad([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string fileName, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string FileType) {
            object[] results = this.Invoke("DownLoad", new object[] {
                        fileName,
                        FileType});
            return ((byte[])(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginDownLoad(string fileName, string FileType, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("DownLoad", new object[] {
                        fileName,
                        FileType}, callback, asyncState);
        }
        
        /// <remarks/>
        public byte[] EndDownLoad(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((byte[])(results[0]));
        }
    }
}