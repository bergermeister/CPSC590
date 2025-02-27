﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GAWebHost.Servant2 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Servant2.IWorker", CallbackContract=typeof(GAWebHost.Servant2.IWorkerCallback))]
    public interface IWorker {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IWorker/MRun")]
        void MRun(GAWebLib.Worker aoWorker, double[][] adDistMat, int aiIterations);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IWorker/MRun")]
        System.Threading.Tasks.Task MRunAsync(GAWebLib.Worker aoWorker, double[][] adDistMat, int aiIterations);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWorkerCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IWorker/MOnComplete")]
        void MOnComplete(GAWebLib.Worker aoWorker);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWorkerChannel : GAWebHost.Servant2.IWorker, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WorkerClient : System.ServiceModel.DuplexClientBase<GAWebHost.Servant2.IWorker>, GAWebHost.Servant2.IWorker {
        
        public WorkerClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public WorkerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public WorkerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public WorkerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public WorkerClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void MRun(GAWebLib.Worker aoWorker, double[][] adDistMat, int aiIterations) {
            base.Channel.MRun(aoWorker, adDistMat, aiIterations);
        }
        
        public System.Threading.Tasks.Task MRunAsync(GAWebLib.Worker aoWorker, double[][] adDistMat, int aiIterations) {
            return base.Channel.MRunAsync(aoWorker, adDistMat, aiIterations);
        }
    }
}
