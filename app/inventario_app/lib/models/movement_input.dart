class MovementInput {
  final double quantity;
  final String productId;
  final int type;

  MovementInput({
    required this.quantity,
    required this.productId,
    required this.type,
  });

  factory MovementInput.fromJson(Map<String, dynamic> json) {
    return MovementInput(
      quantity: (json['quantity'] as num).toDouble(),
      productId: json['productId'],
      type: json['type'],
    );
  }

  Map<String, dynamic> toJson() {
    return {'quantity': quantity, 'productId': productId, 'type': type};
  }
}
