class CategoryInput {
  final String name;

  CategoryInput({required this.name});

  factory CategoryInput.fromJson(Map<String, dynamic> json) {
    return CategoryInput(name: json['name']);
  }

  Map<String, dynamic> toJson() {
    return {'name': name};
  }
}
