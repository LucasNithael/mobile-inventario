import 'dart:convert';

import 'package:http/http.dart' as http;
import 'package:inventario_app/models/category_input.dart';
import 'package:inventario_app/models/category_output.dart';
import 'package:inventario_app/config/api_config.dart';

class CategoryRepository {
  final String baseUrl = ApiConfig.baseUrl;

  Future<List<CategoryOutput>> getAll() async {
    final response = await http.get(Uri.parse('$baseUrl/api/category'));

    final Map<String, dynamic> json = jsonDecode(response.body);
    final List list = json['data'];
    return list.map((e) => CategoryOutput.fromJson(e)).toList();
  }

  Future<void> create(CategoryInput input) async {
    await http.post(
      Uri.parse('$baseUrl/api/category'),
      headers: {'Content-Type': 'application/json'},
      body: jsonEncode(input.toJson()),
    );
  }

  Future<void> update(String id, CategoryInput input) async {
    await http.put(
      Uri.parse('$baseUrl/api/category/$id'),
      headers: {'Content-Type': 'application/json'},
      body: jsonEncode(input.toJson()),
    );
  }

  Future<void> delete(String id) async {
    await http.delete(Uri.parse('$baseUrl/api/category/$id'));
  }
}
