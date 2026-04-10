import 'dart:convert';

import 'package:http/http.dart' as http;
import 'package:inventario_app/models/product_input.dart';
import 'package:inventario_app/models/product_output.dart';
import 'package:inventario_app/config/api_config.dart';

class ProductRepository {
  final String baseUrl = ApiConfig.baseUrl;

  Future<List<ProductOutput>> getAll() async {
    final response = await http.get(Uri.parse('$baseUrl/api/product'));

    final Map<String, dynamic> json = jsonDecode(response.body);
    final List list = json['data'];

    return list.map((e) => ProductOutput.fromJson(e)).toList();
  }

  Future<void> create(ProductInput input) async {
    await http.post(
      Uri.parse('$baseUrl/api/product'),
      headers: {'Content-Type': 'application/json'},
      body: jsonEncode(input.toJson()),
    );
  }

  Future<void> update(String id, ProductInput input) async {
    await http.put(
      Uri.parse('$baseUrl/api/product/$id'),
      headers: {'Content-Type': 'application/json'},
      body: jsonEncode(input.toJson()),
    );
  }

  Future<void> delete(String id) async {
    await http.delete(Uri.parse('$baseUrl/api/product/$id'));
  }
}
