using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ScriptEngine.Machine;
using ScriptEngine.Machine.Contexts;
using System.Web;

using ScriptEngine.HostedScript.Library;
using ScriptEngine.HostedScript.Library.Binary;


namespace OneScript.JrpcClient
{
    [ContextClass("КлиентJrpc", "JrpcClient")]
    public class JrpcClientImpl : AutoContext<JrpcClientImpl>
    {
        System.Collections.Generic.Dictionary<string, int> _methods = new Dictionary<string, int>();
        readonly ScriptEngine.HostedScript.Library.Json.GlobalJsonFunctions _jsonFunctions = (ScriptEngine.HostedScript.Library.Json.GlobalJsonFunctions)ScriptEngine.HostedScript.Library.Json.GlobalJsonFunctions.CreateInstance();
        readonly ScriptEngine.HostedScript.Library.Json.JSONWriter _jsonWriter = new ScriptEngine.HostedScript.Library.Json.JSONWriter();
        readonly ScriptEngine.HostedScript.Library.Json.JSONReader _jsonReader = new ScriptEngine.HostedScript.Library.Json.JSONReader();

        [ContextProperty("Соединение", "Connection")]
        public ScriptEngine.HostedScript.Library.Http.HttpConnectionContext Connection { get; set; }

        [ContextProperty("Запрос", "Request")]
        public ScriptEngine.HostedScript.Library.Http.HttpRequestContext Request { get; set; }

        bool _autogenerateId = true;
        [ContextProperty("АвтогенерацияId", "IdAutogeneration")]
        public IValue AutogenerateId
        {
            get
            {
                return ValueFactory.Create(_autogenerateId);
            }
            set
            {
                _autogenerateId = value.AsBoolean();
            }
        }

        IValue _id = ValueFactory.Create();
        [ContextProperty("Идентификатор", "Identifier")]
        public IValue Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (value.DataType == DataType.Number || value.DataType == DataType.String || value == ValueFactory.Create() || value == null)
                {
                    if (value == null)
                        _id = ValueFactory.Create();
                    else
                        _id = value;
                }
                else
                    throw new Exception("Type is not supported");
            }
        }

        ScriptEngine.HostedScript.Library.Json.JSONWriterSettings _jsonSerializationSettings = null;
        [ContextProperty("НастройкиСериализацииJSON", "JSONSerializationSettings")]
        public IValue JSONSerializationSettings
        {
            get
            {
                if (_jsonSerializationSettings == null)
                    return (IValue)ValueFactory.Create();
                else
                    return _jsonSerializationSettings;
            }
            set
            {
                if (value == ValueFactory.Create() || value == null)
                    _jsonSerializationSettings = null;
                else
                    _jsonSerializationSettings = (ScriptEngine.HostedScript.Library.Json.JSONWriterSettings)value.AsObject();
            }
        }

        string _writeConversionFunction = null;
        [ContextProperty("ФункцияПреобразованияЗаписи", "WriteConversionFunction")]
        public IValue WriteConversionFunction
        {
            get
            {
                if (_writeConversionFunction == null)
                    return ValueFactory.Create();
                else
                    return ValueFactory.Create(_writeConversionFunction);
            }
            set
            {
                if (value == null || value == ValueFactory.Create())
                    _writeConversionFunction = null;
                else
                    _writeConversionFunction = value.AsString();
            }
        }

        IValue _writeConversionModule = null;
        [ContextProperty("МодульПреобразованияЗаписи", "WriteConversionModule")]
        public IValue WriteConversionModule
        {
            get
            {
                if (_writeConversionModule == null)
                    return ValueFactory.Create();
                else
                    return _writeConversionModule;
            }
            set
            {
                if (value == ValueFactory.Create() || value == null)
                    _writeConversionModule = null;
                else if (value.DataType == DataType.String)
                    _writeConversionModule = value;
                else
                    throw new Exception("Type is not supported");
            }
        }

        IValue _conversionFunctionAdditionalParameters = null;
        [ContextProperty("ДополнительныеПараметрыФункцииПреобразования", "ConversionFunctionAdditionalParameters")]
        public IValue ConversionFunctionAdditionalParameters
        {
            get
            {
                if (_conversionFunctionAdditionalParameters == null)
                    return ValueFactory.Create();
                else
                    return _conversionFunctionAdditionalParameters;
            }
            set
            {
                if (value == ValueFactory.Create() || value == null)
                    _conversionFunctionAdditionalParameters = null;
                else 
                    _conversionFunctionAdditionalParameters = value;
            }
        }

        ScriptEngine.HostedScript.Library.Json.JSONDateFormatEnum _readDateFormat = null;
        [ContextProperty("ФорматДатыJSON", "JSONDateFormat")]
        public IValue JSONDateFormat
        {
            get
            {
                if (_readDateFormat == null)
                    return (IValue)ValueFactory.Create();
                else
                    return _readDateFormat;
            }
            set
            {
                if (value == ValueFactory.Create() || value == null)
                    _readDateFormat = null;
                else
                    _readDateFormat = (ScriptEngine.HostedScript.Library.Json.JSONDateFormatEnum)value.AsObject();
            }
        }

        string _recoveryFunction = null;
        [ContextProperty("ФункцияВосстановления", "RecoveryFunction")]
        public IValue RecoveryFunction
        {
            get
            {
                if (_recoveryFunction == null)
                    return ValueFactory.Create();
                else
                    return ValueFactory.Create(_recoveryFunction);
            }
            set
            {
                if (value == null || value == ValueFactory.Create())
                    _recoveryFunction = null;
                else
                    _recoveryFunction = value.AsString();
            }
        }

        IValue _recoveryFunctionModule = null;
        [ContextProperty("МодульФункцииВосстановления", "RecoveryFunctionModule")]
        public IValue RecoveryFunctionModule
        {
            get
            {
                if (_recoveryFunctionModule == null)
                    return ValueFactory.Create();
                else
                    return _recoveryFunctionModule;
            }
            set
            {
                if (value == ValueFactory.Create() || value == null)
                    _recoveryFunctionModule = null;
                else if (value.DataType == DataType.String)
                    _recoveryFunctionModule = value;
                else
                    throw new Exception("Type is not supported");
            }
        }

        ArrayImpl _recoveryProperties = null;
        [ContextProperty("РеквизитыВосстановления", "RecoveryProperties")]
        public IValue RecoveryProperties
        {
            get
            {
                if (_recoveryProperties == null)
                    return ValueFactory.Create();
                else
                    return _recoveryProperties;
            }
            set
            {
                if (value == ValueFactory.Create() || value == null)
                    _recoveryProperties = null;
                else if (value.AsObject() is ArrayImpl || value.AsObject() is FixedArrayImpl)
                    _recoveryProperties = (ArrayImpl)value.AsObject();
                else
                    throw new Exception("Type is not supported");
            }
        }

        IValue _recoveryFunctionAdditionalParameters = null;
        [ContextProperty("ДополнительныеПараметрыФункцииВосстановления", "RecoveryFunctionAdditionalParameters")]
        public IValue RecoveryFunctionAdditionalParameters
        {
            get
            {
                if (_recoveryFunctionAdditionalParameters == null)
                    return ValueFactory.Create();
                else
                    return _recoveryFunctionAdditionalParameters;
            }
            set
            {
                if (value == ValueFactory.Create() || value == null)
                    _recoveryFunctionAdditionalParameters = null;
                else 
                    _recoveryFunctionAdditionalParameters = value;
            }
        }

        [ScriptConstructor(Name = "Без параметров")]
        public static JrpcClientImpl Constructor()
        {
            return new JrpcClientImpl();
        }

        // AutoContext
        /*
        public override bool IsPropReadable(int propNum)
        {
            return _properties.GetProperty(propNum).CanRead;
        }

        public override bool IsPropWritable(int propNum)
        {
            return _properties.GetProperty(propNum).CanWrite;
        }

        public override int FindProperty(string name)
        {
            return _properties.FindProperty(name);
        }

        public override IValue GetPropValue(int propNum)
        {
            try
            {
                return _properties.GetProperty(propNum).Getter((TInstance)this);
            }
            catch (System.Reflection.TargetInvocationException e)
            {
                Debug.Assert(e.InnerException != null);
                throw e.InnerException;
            }
        }

        public override void SetPropValue(int propNum, IValue newVal)
        {
            try
            {
                _properties.GetProperty(propNum).Setter((TInstance)this, newVal);
            }
            catch (System.Reflection.TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }

        public override int GetPropCount()
        {
            return _properties.Count;
        }

        public override string GetPropName(int propNum)
        {
            return _properties.GetProperty(propNum).Name;
        }
        */

        public override bool DynamicMethodSignatures => true;

        public override int FindMethod(string name)
        {
            int value = -1;
            if (!_methods.TryGetValue(name, out value))
                _methods.Add(name, _methods.Count + 1000);

            return _methods.Count + 999;
/*
            try
            {

                return base.FindMethod(name);
            }
            catch
            {
                int value = -1;
                if (!_methods.TryGetValue(name, out value))
                    _methods.Add(name, _methods.Count + 1000);

                return _methods.Count + 999;
            }
*/
            //return _methods.FindMethod(name);
        }
        /*
        public override int GetMethodsCount()
        {
            return _methods.Count;
        }
        */

        public override MethodInfo GetMethodInfo(int methodNumber)
        {
            if (methodNumber < 1000)
                return base.GetMethodInfo(methodNumber);
            else
                return new MethodInfo();
        }

        /*
        private void CheckIfCallIsPossible(int methodNumber, IValue[] arguments)
        {
            var methodInfo = _methods.GetMethodInfo(methodNumber);
            if (!methodInfo.IsDeprecated)
            {
                return;
            }
            if (methodInfo.ThrowOnUseDeprecated)
            {
                throw RuntimeException.DeprecatedMethodCall(methodInfo.Name);
            }
            if (_warnedDeprecatedMethods.Contains(methodNumber))
            {
                return;
            }
            SystemLogger.Write($"ВНИМАНИЕ! Вызов устаревшего метода {methodInfo.Name}");
            _warnedDeprecatedMethods.Add(methodNumber);
        }
        */
        public override void CallAsProcedure(int methodNumber, IValue[] arguments)
        {
            if (methodNumber < 1000)
                base.CallAsProcedure(methodNumber, arguments);
            else
            {
                if(_autogenerateId)
                {
                    _id = ValueFactory.Create((System.Guid.NewGuid()).ToString());
                }

                StructureImpl requestBody = new StructureImpl();
                requestBody.Insert("jsonrpc", ValueFactory.Create("2.0"));
                requestBody.Insert("id", _id);
                requestBody.Insert("method", ValueFactory.Create(_methods.FirstOrDefault(x => x.Value == methodNumber).Key));

                ArrayImpl args = new ArrayImpl();
                foreach(IValue ca in arguments)
                {
                    args.Add(ca);
                }

                requestBody.Insert("params", args);
                _jsonWriter.SetString();
                _jsonFunctions.WriteJSON(_jsonWriter, requestBody, _jsonSerializationSettings, _writeConversionFunction, _writeConversionModule, _conversionFunctionAdditionalParameters);

                string bodyStr = _jsonWriter.Close();
                //System.IO.StreamWriter sw = new System.IO.StreamWriter("c:\\1\\body.txt");
                //sw.Write(bodyStr);
                //sw.Close();
                Request.SetBodyFromString(bodyStr);

                ScriptEngine.HostedScript.Library.Http.HttpResponseContext response = Connection.Post(Request);

                if(response.StatusCode != 200)
                {
                    throw new Exception("HTTP: " + response.StatusCode.ToString() + " " + response.GetBodyAsString().ToString());
                }

                if (_id == ValueFactory.Create())
                {
                    return;
                }
                else
                {
                    _jsonReader.SetString(response.GetBodyAsString().ToString());
                    StructureImpl responseBody = (StructureImpl)_jsonFunctions.ReadJSON(_jsonReader, false, null, _readDateFormat, _recoveryFunction, _recoveryFunctionModule, _recoveryFunctionAdditionalParameters, _recoveryProperties);

                    _jsonReader.Close();

                    if (responseBody.HasProperty("error"))
                    {
                        StructureImpl error = (StructureImpl)responseBody.GetPropValue("error");
                        throw new Exception("Code: " + error.GetPropValue("code").ToString() + " " + error.GetPropValue("message").ToString());
                    }
                }
            }
        }

        public override void CallAsFunction(int methodNumber, IValue[] arguments, out IValue retValue)
        {
            if (methodNumber < 1000)
                base.CallAsFunction(methodNumber, arguments, out retValue);
            else
            {
                if (_autogenerateId)
                {
                    _id = ValueFactory.Create((System.Guid.NewGuid()).ToString());
                }

                StructureImpl requestBody = new StructureImpl();
                requestBody.Insert("jsonrpc", ValueFactory.Create("2.0"));
                requestBody.Insert("id", _id);
                requestBody.Insert("method", ValueFactory.Create(_methods.FirstOrDefault(x => x.Value == methodNumber).Key));

                ArrayImpl args = new ArrayImpl();
                foreach (IValue ca in arguments)
                {
                    args.Add(ca);
                }

                requestBody.Insert("params", args);
                _jsonWriter.SetString();
                _jsonFunctions.WriteJSON(_jsonWriter, requestBody, _jsonSerializationSettings, _writeConversionFunction, _writeConversionModule, _conversionFunctionAdditionalParameters);
                string bodyStr = _jsonWriter.Close();
                System.IO.StreamWriter sw = new System.IO.StreamWriter("c:\\1\\body.txt");
                sw.Write(bodyStr);
                sw.Close();
                Request.SetBodyFromString(bodyStr);

                Request.SetBodyFromString(bodyStr);
                ScriptEngine.HostedScript.Library.Http.HttpResponseContext response = Connection.Post(Request);

                if (response.StatusCode != 200)
                {
                    throw new Exception("HTTP: " + response.StatusCode.ToString() + " " + response.GetBodyAsString().ToString());
                }

                if (_id == ValueFactory.Create())
                {
                    retValue = ValueFactory.Create();
                    return;
                }
                else
                {
                    _jsonReader.SetString(response.GetBodyAsString().ToString());
                    StructureImpl responseBody = (StructureImpl)_jsonFunctions.ReadJSON(_jsonReader, false, null, _readDateFormat, _recoveryFunction, _recoveryFunctionModule, _recoveryFunctionAdditionalParameters, _recoveryProperties);
                    _jsonReader.Close();

                    if (responseBody.HasProperty("error"))
                    {
                        StructureImpl error = (StructureImpl)responseBody.GetPropValue("error");
                        throw new Exception("Code: " + error.GetPropValue("code").ToString() + " " + error.GetPropValue("message").ToString());
                    }
                    else
                    {
                        retValue = responseBody.GetPropValue("result");
                    }
                }
            }
        }
    }
}
