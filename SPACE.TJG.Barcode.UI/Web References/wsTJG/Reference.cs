﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.8670
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.CompactFramework.Design.Data, Version 2.0.50727.8670.
// 
namespace UserInterfaces.wsTJG {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    using System.Data;
    
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="BasicHttpBinding_IService", Namespace="http://tempuri.org/")]
    public partial class Service : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        /// <remarks/>
        public Service() {
            this.Url = "http://192.168.1.36/TJGService/Service.svc";
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IService/GetData", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string GetData(int value, [System.Xml.Serialization.XmlIgnoreAttribute()] bool valueSpecified) {
            object[] results = this.Invoke("GetData", new object[] {
                        value,
                        valueSpecified});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetData(int value, bool valueSpecified, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetData", new object[] {
                        value,
                        valueSpecified}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndGetData(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IService/GetDataUsingDataContract", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public CompositeType GetDataUsingDataContract([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] CompositeType composite) {
            object[] results = this.Invoke("GetDataUsingDataContract", new object[] {
                        composite});
            return ((CompositeType)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetDataUsingDataContract(CompositeType composite, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetDataUsingDataContract", new object[] {
                        composite}, callback, asyncState);
        }
        
        /// <remarks/>
        public CompositeType EndGetDataUsingDataContract(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((CompositeType)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IService/ConnectDB", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void ConnectDB(out bool ConnectDBResult, [System.Xml.Serialization.XmlIgnoreAttribute()] out bool ConnectDBResultSpecified) {
            object[] results = this.Invoke("ConnectDB", new object[0]);
            ConnectDBResult = ((bool)(results[0]));
            ConnectDBResultSpecified = ((bool)(results[1]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginConnectDB(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ConnectDB", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public void EndConnectDB(System.IAsyncResult asyncResult, out bool ConnectDBResult, out bool ConnectDBResultSpecified) {
            object[] results = this.EndInvoke(asyncResult);
            ConnectDBResult = ((bool)(results[0]));
            ConnectDBResultSpecified = ((bool)(results[1]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IService/SqlConn", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public object SqlConn() {
            object[] results = this.Invoke("SqlConn", new object[0]);
            return ((object)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginSqlConn(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("SqlConn", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public object EndSqlConn(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((object)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IService/GetDateServer", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public object GetDateServer() {
            object[] results = this.Invoke("GetDateServer", new object[0]);
            return ((object)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetDateServer(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetDateServer", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public object EndGetDateServer(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((object)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IService/IsOnline", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string IsOnline() {
            object[] results = this.Invoke("IsOnline", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginIsOnline(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("IsOnline", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public string EndIsOnline(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IService/InsUpdDistributionRet", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string InsUpdDistributionRet([System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", IsNullable=true)] byte[] Data) {
            object[] results = this.Invoke("InsUpdDistributionRet", new object[] {
                        Data});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginInsUpdDistributionRet(byte[] Data, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("InsUpdDistributionRet", new object[] {
                        Data}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndInsUpdDistributionRet(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IService/DoInsUpdDistributionRet", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string DoInsUpdDistributionRet([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string serial_number, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string cust_id, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string doc_no, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string vehicle, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string empty_or_full, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string create_date) {
            object[] results = this.Invoke("DoInsUpdDistributionRet", new object[] {
                        serial_number,
                        cust_id,
                        doc_no,
                        vehicle,
                        empty_or_full,
                        create_date});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginDoInsUpdDistributionRet(string serial_number, string cust_id, string doc_no, string vehicle, string empty_or_full, string create_date, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("DoInsUpdDistributionRet", new object[] {
                        serial_number,
                        cust_id,
                        doc_no,
                        vehicle,
                        empty_or_full,
                        create_date}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndDoInsUpdDistributionRet(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IService/InsUpdGIDelivery", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string InsUpdGIDelivery([System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", IsNullable=true)] byte[] Data) {
            object[] results = this.Invoke("InsUpdGIDelivery", new object[] {
                        Data});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginInsUpdGIDelivery(byte[] Data, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("InsUpdGIDelivery", new object[] {
                        Data}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndInsUpdGIDelivery(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IService/DoInsUpdGIDelivery", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string DoInsUpdGIDelivery([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string serial_number, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string do_no, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string create_date) {
            object[] results = this.Invoke("DoInsUpdGIDelivery", new object[] {
                        serial_number,
                        do_no,
                        create_date});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginDoInsUpdGIDelivery(string serial_number, string do_no, string create_date, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("DoInsUpdGIDelivery", new object[] {
                        serial_number,
                        do_no,
                        create_date}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndDoInsUpdGIDelivery(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IService/InsUpdGIRefIOEmpty", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string InsUpdGIRefIOEmpty([System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", IsNullable=true)] byte[] Data) {
            object[] results = this.Invoke("InsUpdGIRefIOEmpty", new object[] {
                        Data});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginInsUpdGIRefIOEmpty(byte[] Data, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("InsUpdGIRefIOEmpty", new object[] {
                        Data}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndInsUpdGIRefIOEmpty(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IService/DoInsGIRefIOEmpty", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string DoInsGIRefIOEmpty([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string serial_number, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string do_no, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string create_date) {
            object[] results = this.Invoke("DoInsGIRefIOEmpty", new object[] {
                        serial_number,
                        do_no,
                        create_date});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginDoInsGIRefIOEmpty(string serial_number, string do_no, string create_date, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("DoInsGIRefIOEmpty", new object[] {
                        serial_number,
                        do_no,
                        create_date}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndDoInsGIRefIOEmpty(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IService/InsUpdGRProduction", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string InsUpdGRProduction([System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", IsNullable=true)] byte[] Data) {
            object[] results = this.Invoke("InsUpdGRProduction", new object[] {
                        Data});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginInsUpdGRProduction(byte[] Data, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("InsUpdGRProduction", new object[] {
                        Data}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndInsUpdGRProduction(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IService/DoInsGRProduction", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string DoInsGRProduction([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string serial_number, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string pre_order, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string batch, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string create_date) {
            object[] results = this.Invoke("DoInsGRProduction", new object[] {
                        serial_number,
                        pre_order,
                        batch,
                        create_date});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginDoInsGRProduction(string serial_number, string pre_order, string batch, string create_date, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("DoInsGRProduction", new object[] {
                        serial_number,
                        pre_order,
                        batch,
                        create_date}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndDoInsGRProduction(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IService/DoLoadCustomer", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Data.DataTable DoLoadCustomer() {
            object[] results = this.Invoke("DoLoadCustomer", new object[0]);
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginDoLoadCustomer(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("DoLoadCustomer", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataTable EndDoLoadCustomer(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IService/DoLoadCustomerByte", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public ServiceResponse DoLoadCustomerByte() {
            object[] results = this.Invoke("DoLoadCustomerByte", new object[0]);
            return ((ServiceResponse)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginDoLoadCustomerByte(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("DoLoadCustomerByte", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public ServiceResponse EndDoLoadCustomerByte(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((ServiceResponse)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IService/DoLoadCustomerStored", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Data.DataTable DoLoadCustomerStored() {
            object[] results = this.Invoke("DoLoadCustomerStored", new object[0]);
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginDoLoadCustomerStored(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("DoLoadCustomerStored", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataTable EndDoLoadCustomerStored(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataTable)(results[0]));
        }
    }
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.datacontract.org/2004/07/iWebServices")]
    public partial class CompositeType {
        
        private bool boolValueField;
        
        private bool boolValueFieldSpecified;
        
        private string stringValueField;
        
        /// <remarks/>
        public bool BoolValue {
            get {
                return this.boolValueField;
            }
            set {
                this.boolValueField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BoolValueSpecified {
            get {
                return this.boolValueFieldSpecified;
            }
            set {
                this.boolValueFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string StringValue {
            get {
                return this.stringValueField;
            }
            set {
                this.stringValueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.datacontract.org/2004/07/iWebServices.Objects")]
    public partial class ServiceResponse {
        
        private string codeField;
        
        private byte[] dataField;
        
        private string detailField;
        
        private string idField;
        
        private System.Data.DataSet dsField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string Code {
            get {
                return this.codeField;
            }
            set {
                this.codeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", IsNullable=true)]
        public byte[] Data {
            get {
                return this.dataField;
            }
            set {
                this.dataField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string Detail {
            get {
                return this.detailField;
            }
            set {
                this.detailField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Data.DataSet ds {
            get {
                return this.dsField;
            }
            set {
                this.dsField = value;
            }
        }
    }
}