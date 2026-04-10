class ProductInput {
  final String name;
  final double minimumQuantity;
  final String categoryId;

  ProductInput({
    required this.name,
    required this.minimumQuantity,
    required this.categoryId,
  });

  factory ProductInput.fromJson(Map<String, dynamic> json) {
    return ProductInput(
      name: json['name'],
      minimumQuantity: (json['minimumQuantity'] as num).toDouble(),
      categoryId: json['categoryId'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'name': name,
      'minimumQuantity': minimumQuantity,
      'categoryId': categoryId,
    };
  }
}