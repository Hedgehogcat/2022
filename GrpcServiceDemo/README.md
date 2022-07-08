# 2022
api 采用nginx 来配置 ca证书 确保https
api 和grpc之间由于使用内网 可以使用 token 也可以不使用直接使用http请求 
htttp http2 http3 都属于http请求，只是传输内容的格式不一样 https 是http +ssl 
前段只会看到api的借口，api调用grpc的内容不会看到
可以讲api grpc 的调用走jaeger 进入联络追踪查看信息流