class CategoryOutput {
  final String id;
  final String name;
  final double productQuantity;
  final DateTime created;

  CategoryOutput({
    required this.id,
    required this.name,
    required this.productQuantity,
    required this.created,
  });

  factory CategoryOutput.fromJson(Map<String, dynamic> json) {
    return CategoryOutput(
      id: json['id'],
      name: json['name'],
      productQuantity: json['productQuantity'],
      created: json['created'],
    );
  }
}
