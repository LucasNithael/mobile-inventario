class ProductOutput {
  final String id;
  final String name;
  final String categoryId;
  final String category;
  final double quantity;
  final double minimumQuantity;
  final DateTime created;

  ProductOutput({
    required this.id,
    required this.name,
    required this.categoryId,
    required this.category,
    required this.quantity,
    required this.minimumQuantity,
    required this.created,
  });

  factory ProductOutput.fromJson(Map<String, dynamic> json) {
    return ProductOutput(
      id: json['id'],
      name: json['name'],
      categoryId: json['categoryId'],
      category: json['category'],
      quantity: (json['quantity'] as num).toDouble(),
      minimumQuantity: (json['minimumQuantity'] as num).toDouble(),
      created: DateTime.parse(json['created']),
    );
  }
}