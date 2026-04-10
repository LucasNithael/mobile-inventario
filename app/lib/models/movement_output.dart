class MovementOutput {
  final String id;
  final double quantity;
  final String productId;
  final String product;
  final int type;
  final DateTime created;

  MovementOutput({
    required this.id,
    required this.quantity,
    required this.productId,
    required this.product,
    required this.type,
    required this.created,
  });

  factory MovementOutput.fromJson(Map<String, dynamic> json) {
    return MovementOutput(
      id: json['id'],
      quantity: (json['quantity'] as num).toDouble(),
      productId: json['productId'],
      product: json['product'],
      type: json['type'],
      created: DateTime.parse(json['created']),
    );
  }
}