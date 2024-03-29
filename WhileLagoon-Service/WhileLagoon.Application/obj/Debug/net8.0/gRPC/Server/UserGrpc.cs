// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: gRPC/Server/User.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981, 0612
#region Designer generated code

using grpc = global::Grpc.Core;

namespace UserGRPCService {
  public static partial class UserGRPC
  {
    static readonly string __ServiceName = "UserGRPC.UserGRPC";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::UserGRPCService.GetUserReq> __Marshaller_UserGRPC_GetUserReq = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::UserGRPCService.GetUserReq.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::UserGRPCService.GetUserRes> __Marshaller_UserGRPC_GetUserRes = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::UserGRPCService.GetUserRes.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::UserGRPCService.VerifyAccessTokenReq> __Marshaller_UserGRPC_VerifyAccessTokenReq = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::UserGRPCService.VerifyAccessTokenReq.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::UserGRPCService.VerifyAccessTokenRes> __Marshaller_UserGRPC_VerifyAccessTokenRes = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::UserGRPCService.VerifyAccessTokenRes.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::UserGRPCService.GetUserReq, global::UserGRPCService.GetUserRes> __Method_GetUser = new grpc::Method<global::UserGRPCService.GetUserReq, global::UserGRPCService.GetUserRes>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetUser",
        __Marshaller_UserGRPC_GetUserReq,
        __Marshaller_UserGRPC_GetUserRes);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::UserGRPCService.VerifyAccessTokenReq, global::UserGRPCService.VerifyAccessTokenRes> __Method_VerifyAccessToken = new grpc::Method<global::UserGRPCService.VerifyAccessTokenReq, global::UserGRPCService.VerifyAccessTokenRes>(
        grpc::MethodType.Unary,
        __ServiceName,
        "VerifyAccessToken",
        __Marshaller_UserGRPC_VerifyAccessTokenReq,
        __Marshaller_UserGRPC_VerifyAccessTokenRes);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::UserGRPCService.UserReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of UserGRPC</summary>
    [grpc::BindServiceMethod(typeof(UserGRPC), "BindService")]
    public abstract partial class UserGRPCBase
    {
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::UserGRPCService.GetUserRes> GetUser(global::UserGRPCService.GetUserReq request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::UserGRPCService.VerifyAccessTokenRes> VerifyAccessToken(global::UserGRPCService.VerifyAccessTokenReq request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(UserGRPCBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_GetUser, serviceImpl.GetUser)
          .AddMethod(__Method_VerifyAccessToken, serviceImpl.VerifyAccessToken).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, UserGRPCBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_GetUser, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::UserGRPCService.GetUserReq, global::UserGRPCService.GetUserRes>(serviceImpl.GetUser));
      serviceBinder.AddMethod(__Method_VerifyAccessToken, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::UserGRPCService.VerifyAccessTokenReq, global::UserGRPCService.VerifyAccessTokenRes>(serviceImpl.VerifyAccessToken));
    }

  }
}
#endregion
