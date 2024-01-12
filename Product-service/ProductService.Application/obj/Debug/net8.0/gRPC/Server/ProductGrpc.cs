// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: gRPC/Server/Product.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981, 0612
#region Designer generated code

using grpc = global::Grpc.Core;

namespace ProductService {
  public static partial class ProductGRPC
  {
    static readonly string __ServiceName = "ProductGRPC.ProductGRPC";

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
    static readonly grpc::Marshaller<global::ProductService.GetProductReq> __Marshaller_ProductGRPC_GetProductReq = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProductService.GetProductReq.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProductService.GetProductRes> __Marshaller_ProductGRPC_GetProductRes = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProductService.GetProductRes.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProductService.ProductIds> __Marshaller_ProductGRPC_ProductIds = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProductService.ProductIds.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProductService.Products> __Marshaller_ProductGRPC_Products = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProductService.Products.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProductService.GetProductsReq> __Marshaller_ProductGRPC_GetProductsReq = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProductService.GetProductsReq.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProductService.GetPriceRes> __Marshaller_ProductGRPC_GetPriceRes = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProductService.GetPriceRes.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::ProductService.GetProductReq, global::ProductService.GetProductRes> __Method_GetProduct = new grpc::Method<global::ProductService.GetProductReq, global::ProductService.GetProductRes>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetProduct",
        __Marshaller_ProductGRPC_GetProductReq,
        __Marshaller_ProductGRPC_GetProductRes);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::ProductService.ProductIds, global::ProductService.Products> __Method_GetProductByIds = new grpc::Method<global::ProductService.ProductIds, global::ProductService.Products>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetProductByIds",
        __Marshaller_ProductGRPC_ProductIds,
        __Marshaller_ProductGRPC_Products);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::ProductService.GetProductsReq, global::ProductService.GetPriceRes> __Method_GetPrices = new grpc::Method<global::ProductService.GetProductsReq, global::ProductService.GetPriceRes>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetPrices",
        __Marshaller_ProductGRPC_GetProductsReq,
        __Marshaller_ProductGRPC_GetPriceRes);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::ProductService.ProductReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of ProductGRPC</summary>
    [grpc::BindServiceMethod(typeof(ProductGRPC), "BindService")]
    public abstract partial class ProductGRPCBase
    {
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::ProductService.GetProductRes> GetProduct(global::ProductService.GetProductReq request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::ProductService.Products> GetProductByIds(global::ProductService.ProductIds request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::ProductService.GetPriceRes> GetPrices(global::ProductService.GetProductsReq request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(ProductGRPCBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_GetProduct, serviceImpl.GetProduct)
          .AddMethod(__Method_GetProductByIds, serviceImpl.GetProductByIds)
          .AddMethod(__Method_GetPrices, serviceImpl.GetPrices).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, ProductGRPCBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_GetProduct, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::ProductService.GetProductReq, global::ProductService.GetProductRes>(serviceImpl.GetProduct));
      serviceBinder.AddMethod(__Method_GetProductByIds, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::ProductService.ProductIds, global::ProductService.Products>(serviceImpl.GetProductByIds));
      serviceBinder.AddMethod(__Method_GetPrices, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::ProductService.GetProductsReq, global::ProductService.GetPriceRes>(serviceImpl.GetPrices));
    }

  }
}
#endregion
