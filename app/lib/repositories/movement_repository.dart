import 'dart:convert';

import 'package:http/http.dart' as http;
import 'package:inventario_app/models/movement_input.dart';
import 'package:inventario_app/models/movement_output.dart';
import 'package:inventario_app/config/api_config.dart';

class MovementRepository {
  final String baseUrl = ApiConfig.baseUrl;

  Future<List<MovementOutput>> getAll() async {
    final response = await http.get(Uri.parse('$baseUrl/api/movement'));

    final Map<String, dynamic> json = jsonDecode(response.body);
    final List list = json['data'];

    return list.map((e) => MovementOutput.fromJson(e)).toList();
  }

  Future<List<MovementOutput>> getByProductId(String productId) async {
    final response = await http.get(
      Uri.parse('$baseUrl/api/movement/product/$productId'),
    );

    final data = jsonDecode(response.body);
    return (data as List).map((e) => MovementOutput.fromJson(e)).toList();
  }

  Future<void> create(MovementInput input) async {
    await http.post(
      Uri.parse('$baseUrl/api/movement'),
      headers: {'Content-Type': 'application/json'},
      body: jsonEncode(input.toJson()),
    );
  }
}
