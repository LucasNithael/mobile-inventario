import 'package:flutter/material.dart';
import '../models/product_output.dart';

class ProductCard extends StatelessWidget {
  final ProductOutput product;
  final VoidCallback onMovement;
  final VoidCallback onEdit;
  final VoidCallback onDelete;

  const ProductCard({
    super.key,
    required this.product,
    required this.onMovement,
    required this.onEdit,
    required this.onDelete,
  });

  bool get isLowStock => product.quantity < product.minimumQuantity;

  @override
  Widget build(BuildContext context) {
    return Card(
      elevation: 0,
      margin: const EdgeInsets.only(bottom: 12),
      shape: RoundedRectangleBorder(
        borderRadius: BorderRadius.circular(18),
      ),
      child: Padding(
        padding: const EdgeInsets.all(14),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            // Nome + warning
            Row(
              children: [
                Expanded(
                  child: Text(
                    product.name,
                    style: const TextStyle(
                      fontSize: 17,
                      fontWeight: FontWeight.w600,
                    ),
                    overflow: TextOverflow.ellipsis,
                  ),
                ),
                if (isLowStock)
                  const Icon(
                    Icons.warning_amber_rounded,
                    color: Colors.amber,
                  ),
              ],
            ),

            const SizedBox(height: 10),

            // Categoria + qtd + icons
            Row(
              children: [
                Container(
                  padding:
                      const EdgeInsets.symmetric(horizontal: 12, vertical: 6),
                  decoration: BoxDecoration(
                    color: Colors.grey.withOpacity(0.2),
                    borderRadius: BorderRadius.circular(20),
                  ),
                  child: Text(
                    product.category,
                    style: const TextStyle(fontSize: 12),
                  ),
                ),

                const SizedBox(width: 12),

                Text(
                  "Qtd: ${product.quantity}",
                  style: const TextStyle(fontWeight: FontWeight.w500),
                ),

                const Spacer(),

                IconButton(
                  onPressed: onMovement,
                  icon: const Icon(Icons.swap_horiz),
                ),
                IconButton(
                  onPressed: onEdit,
                  icon: const Icon(Icons.edit_outlined),
                ),
                IconButton(
                  onPressed: onDelete,
                  icon: const Icon(Icons.delete_outline, color: Colors.red),
                ),
              ],
            ),

            const SizedBox(height: 4),

            Text(
              "Min: ${product.minimumQuantity}",
              style: TextStyle(
                color: Colors.grey.shade400,
              ),
            ),
          ],
        ),
      ),
    );
  }
}